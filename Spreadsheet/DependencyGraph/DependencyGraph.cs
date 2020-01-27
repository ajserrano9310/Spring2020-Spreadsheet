// Author: Alejandro Rubio
// Date: 1/24/2020
// This file is part of a Library that creates a list of dependees and dependents
// I pledge that I did this work myself
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SpreadsheetUtilities
{
    /// <summary>
    /// (s1,t1) is an ordered pair of strings
    /// t1 depends on s1; s1 must be evaluated before t1
    /// 
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
    /// Recall that sets never contain duplicates.  If an attempt is made to add an element to a 
    /// set, and the element is already in the set, the set remains unchanged.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
    ///        (The set of things that depend on s)    
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
    ///        (The set of things that s depends on) 
    //
    // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    //     dependents("a") = {"b", "c"}
    //     dependents("b") = {"d"}
    //     dependents("c") = {}
    //     dependents("d") = {"d"}
    //     dependees("a") = {}
    //     dependees("b") = {"a"}
    //     dependees("c") = {"a"}
    //     dependees("d") = {"b", "d"}
    /// </summary>
    public class DependencyGraph
    {
        private Dictionary<string, HashSet<string>> dependees;
        private Dictionary<string, HashSet<string>> dependents;
        private int pairsSize;
        /// <summary>
        /// Creates an empty DependencyGraph.
        /// </summary>
        public DependencyGraph()
        {
            dependees = new Dictionary<string, HashSet<string>>();
            dependents = new Dictionary<string, HashSet<string>>();
            pairsSize = 0;
        }
        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { 
                return pairsSize; 
            }
        }
        /// <summary>
        /// The size of dependees(s).
        /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
        /// invoke it like this:
        /// dg["a"]
        /// It should return the size of dependees("a")
        /// </summary>
        public int this[string s]
        {
            get {
                // If it contains the key we can get the size
                // If it does not contain the key we just return 0
                if (dependents.ContainsKey(s))
                {
                    return dependents[s].Count;
                }
                return 0; 
            }
        }
        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            if (dependees.ContainsKey(s))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        public bool HasDependees(string s)
        {
            if (dependents.ContainsKey(s))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            HashSet<string> dependentsList = new HashSet<string>();
            // If it contains the key we can get the values
            if (dependees.ContainsKey(s))
            {
                // Iterate trough all the values
                for (int i = 0; i < dependees[s].Count; i++)
                {
                    {
                        // Add them to our hashset
                        dependentsList.Add(dependees[s].ElementAt(i));
                    }
                }
            }
            return dependentsList;
        }
        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            HashSet<string> dependeesList = new HashSet<string>();
            // If it contains the key we can get the values
            if (dependents.ContainsKey(s))
            {
                // Iterate trough all the values
                for (int i = 0; i < dependents[s].Count; i++)
                {
                    {
                        // Add them to our hashset
                        dependeesList.Add(dependents[s].ElementAt(i));
                    }
                }
            }
            return dependeesList;
        }
        /// <summary>
        /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
        /// 
        /// <para>This should be thought of as:</para>   
        /// 
        ///   t depends on s
        ///
        /// </summary>
        /// <param name="s"> s must be evaluated first. T depends on S</param>
        /// <param name="t"> t cannot be evaluated until s is</param>        /// 
        public void AddDependency(string s, string t)
        {
            // Increase the number of pairs
            if (!(dependents.ContainsKey(t) && dependees.ContainsKey(s)))
            {
                pairsSize++;
            }
            // If dependents does not contain key t we have to create a new hashset for those keys
            if (!dependents.ContainsKey(t))
                {
                    HashSet<string> actualDependee = new HashSet<string>();
                    actualDependee.Add(s);
                    dependents.Add(t, actualDependee);
                }
                // If it contains key t we just add it to the hashset
                else
                {
                    dependents[t].Add(s);
                }
            // If dependees does not contain key s we have to create a new hashset for those keys
            if (!dependees.ContainsKey(s))
                {
                    HashSet<string> actualDependent = new HashSet<string>();
                    actualDependent.Add(t);
                    dependees.Add(s, actualDependent);
                }
            // If it contains key s we just add it to the hashset
                else
                {
                    dependees[s].Add(t);
                }
        }
        /// <summary>
        /// Removes the ordered pair (s,t), if it exists
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public void RemoveDependency(string s, string t)
        {
            // Decrease the number of pairs
            if ((dependents.ContainsKey(t) && dependees.ContainsKey(s)))
            {
                pairsSize--;
                // Remove dependency from dependees and dependents
                if (dependees[s].Contains(t))
                {
                    dependees[s].Remove(t);
                    // If the list of dependees or dependents is empty we have to erase it to prevent null checking
                    if (dependees[s].Count == 0)
                    {
                        dependees.Remove(s);
                    }
                }
                if (dependents[t].Contains(s))
                {
                    dependents[t].Remove(s);
                    // If the list of dependees or dependents is empty we have to erase it to prevent null checking
                    if (dependents[t].Count == 0)
                    {
                        dependents.Remove(t);
                    }
                }
            }
        }
        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            // Get the list of dependents
            HashSet<string> actual = (HashSet<string>)GetDependents(s);
            // Remove the old dependents
            for (int i = 0; i < actual.Count(); i++)
            {
                RemoveDependency(s,actual.ElementAt(i));
            }
            // Add the new dependents
            for (int j = 0; j < actual.Count(); j++)
            {
                AddDependency(s,newDependents.ElementAt(j));
            }
        }
        /// <summary>
        /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
        /// t in newDependees, adds the ordered pair (t,s).
        /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            // Get the list of dependees
            IEnumerable<string> actual = GetDependees(s);
            // Remove the old dependees
            for(int i =0; i < actual.Count(); i++)
            {
                RemoveDependency(actual.ElementAt(i), s);
            }
            // Add the new dependees
            for (int j = 0; j < actual.Count(); j++)
            {
                AddDependency(newDependees.ElementAt(j), s);
            }
        }
    }
}
