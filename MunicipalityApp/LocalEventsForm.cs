using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalityApp
{
    public partial class LocalEventsForm : Form
    {
        private SortedDictionary<DateTime, List<EventManager>> eventsByDate;
        private HashSet<string> uniqueCategories;
        private Dictionary<string, List<EventManager>> eventsByCategory;
        private CustomPriorityQueue<EventManager> upcomingEvents;
        private List<string> userSearchHistory;
        private List<EventManager> allEvents;
        private const int MaxSearchHistorySize = 10;

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        public LocalEventsForm()
        {
            InitializeComponent();
            InitializeEventsData();
            userSearchHistory = new List<string>();
            allEvents = eventsByDate.Values.SelectMany(x => x).ToList();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Initialize event data
        /// </summary>
        private void InitializeEventsData()
        {
            eventsByDate = new SortedDictionary<DateTime, List<EventManager>>();
            uniqueCategories = new HashSet<string>();
            eventsByCategory = new Dictionary<string, List<EventManager>>();
            upcomingEvents = new CustomPriorityQueue<EventManager>((a, b) => a.Date.CompareTo(b.Date));

            AddEvent(new EventManager("Music Festival", new DateTime(2024, 10, 20), "Music"));
            AddEvent(new EventManager("Art Exhibition", new DateTime(2024, 10, 22), "Art"));
            AddEvent(new EventManager("Marathon", new DateTime(2024, 10, 25), "Sports"));
            AddEvent(new EventManager("Food Fair", new DateTime(2024, 11, 5), "Food"));
            AddEvent(new EventManager("Tech Conference", new DateTime(2024, 11, 10), "Technology"));
            AddEvent(new EventManager("Book Fair", new DateTime(2024, 11, 15), "Literature"));
            AddEvent(new EventManager("Film Festival", new DateTime(2024, 11, 20), "Film"));
            AddEvent(new EventManager("Charity Run", new DateTime(2024, 11, 25), "Sports"));
            AddEvent(new EventManager("Christmas Market", new DateTime(2024, 12, 1), "Holiday"));
            AddEvent(new EventManager("New Year's Eve Party", new DateTime(2024, 12, 31), "Celebration"));
            AddEvent(new EventManager("Science Expo", new DateTime(2025, 1, 10), "Science"));
            AddEvent(new EventManager("Fashion Show", new DateTime(2025, 1, 20), "Fashion"));
            AddEvent(new EventManager("Gaming Convention", new DateTime(2025, 2, 5), "Gaming"));
            AddEvent(new EventManager("Spring Festival", new DateTime(2025, 3, 1), "Cultural"));
            AddEvent(new EventManager("Easter Egg Hunt", new DateTime(2025, 4, 5), "Holiday"));
            AddEvent(new EventManager("Earth Day Celebration", new DateTime(2025, 4, 22), "Environmental"));
            AddEvent(new EventManager("Summer Camp", new DateTime(2025, 6, 15), "Outdoor"));
            AddEvent(new EventManager("Independence Day Parade", new DateTime(2025, 7, 4), "Holiday"));
            AddEvent(new EventManager("Back to School Fair", new DateTime(2025, 8, 20), "Education"));
            AddEvent(new EventManager("Halloween Party", new DateTime(2025, 10, 31), "Holiday"));
            AddEvent(new EventManager("Thanksgiving Dinner", new DateTime(2025, 11, 27), "Holiday"));

            foreach (var eventList in eventsByDate.Values)
            {
                foreach (var evt in eventList)
                {
                    upcomingEvents.Enqueue(evt);
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBackToMain_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenuForm mainMenu = new MainMenuForm();
            mainMenu.Show();
            // this.Close();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Method to add an event
        /// </summary>
        /// <param name="newEvent"></param>
        private void AddEvent(EventManager newEvent)
        {
            if (!eventsByDate.ContainsKey(newEvent.Date))
            {
                eventsByDate[newEvent.Date] = new List<EventManager>();
            }
            eventsByDate[newEvent.Date].Add(newEvent);

            uniqueCategories.Add(newEvent.Category);

            if (!eventsByCategory.ContainsKey(newEvent.Category))
            {
                eventsByCategory[newEvent.Category] = new List<EventManager>();
            }
            eventsByCategory[newEvent.Category].Add(newEvent);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Search button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Add search query to history, history is only 10 searches big to keep it more recency based
                userSearchHistory.Insert(0, searchQuery);
                if (userSearchHistory.Count > MaxSearchHistorySize)
                {
                    userSearchHistory.RemoveAt(userSearchHistory.Count - 1);
                }

                // Search through events based on the search query
                var searchResults = allEvents.Where(ev =>
                    ev.Name.ToLower().Contains(searchQuery) ||
                    ev.Category.ToLower().Contains(searchQuery) ||
                    ev.Date.ToString("yyyy-MM-dd").Contains(searchQuery)).ToList();

                // Display search results in the first datagridciew
                UpdateDataGridView(dgvSearchResults, searchResults);
            }
            else
            {
                dgvSearchResults.DataSource = null; // Clear search results if the search query is empty
                dgvSearchResults.Refresh();
            }

            // Update recommendations based on search history
            RecommendEvents();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Method to update the datagrid when given a datagrid and a variable
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="events"></param>
        private void UpdateDataGridView(DataGridView dgv, List<EventManager> events)
        {
            dgv.DataSource = null; // Clear existing data
            dgv.DataSource = events.Select(ev => new
            {
                EventName = ev.Name,
                EventDate = ev.Date.ToString("yyyy-MM-dd"),
                EventCategory = ev.Category
            }).ToList();
            dgv.Refresh(); // refresh the datagridview
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Method to recommend events based on search history
        /// </summary>
        private void RecommendEvents()
        {
            if (userSearchHistory.Count == 0)
            {
                dgvRecommendations.DataSource = null;
                dgvRecommendations.Refresh();
                return;
            }

            var weightedSearchTerms = GetWeightedSearchTerms();

            var recommendedEvents = allEvents
                .Select(ev => new
                {
                    Event = ev,
                    Score = weightedSearchTerms.Sum(term =>
                        (ev.Name.ToLower().Contains(term.Key) || ev.Category.ToLower().Contains(term.Key)) ? term.Value : 0)
                })
                .Where(x => x.Score > 0)
                .OrderByDescending(x => x.Score)
                .Select(x => x.Event)
                .Distinct()
                .Take(10)
                .ToList();

            if (recommendedEvents.Count < 10)
            {
                var tempQueue = new CustomPriorityQueue<EventManager>(upcomingEvents);
                var remainingEvents = new List<EventManager>();
                while (tempQueue.Count > 0 && remainingEvents.Count < (10 - recommendedEvents.Count))
                {
                    var nextEvent = tempQueue.Dequeue();
                    if (!recommendedEvents.Contains(nextEvent))
                    {
                        remainingEvents.Add(nextEvent);
                    }
                }
                recommendedEvents.AddRange(remainingEvents);
            }

            UpdateDataGridView(dgvRecommendations, recommendedEvents);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Method to get the weights of the search terms
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, double> GetWeightedSearchTerms()
        {
            var weightedTerms = new Dictionary<string, double>();
            double weight = 1.0; //weight of most recent search
            double weightSum = 0; //sum of all weights

            // look at search history determine which search term should hold the most weight based on recency
            foreach (var term in userSearchHistory)
            {
                if (!weightedTerms.ContainsKey(term)) // check if term exists
                {
                    weightedTerms[term] = weight;
                    weightSum += weight;
                }
                weight *= 0.8; // Reduce the weight for older searches to make the more recent searches actually appear more
            }

            // Normalize weights (make sure the weights add up to 1)
            foreach (var term in weightedTerms.Keys.ToList())
            {
                weightedTerms[term] /= weightSum;
            }

            return weightedTerms;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Method to get random events
        /// </summary>
        /// <param name="events"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private List<EventManager> GetRandomEvents(List<EventManager> events, int count)
        {
            var rng = new Random();
            return events.OrderBy(x => rng.Next()).Take(count).ToList();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void LocalEventsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
