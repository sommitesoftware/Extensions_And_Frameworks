using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sommite.Frameworks.Data
{
    public static class Connections
    {


    }


    /// <summary>
/// Represents the class public.
/// </summary>
public class DotNetPerformanceCounters
    {

        public string GetAssemblyName(Process p)
        {
            return Assembly.GetEntryAssembly().GetName().Name + "[" + p.Id + "]";
            //return "Corris Auswertungs Portal" + "[" + p.Id + "]"; 
        }

        List<PerformanceCounter> lstPerfCounters = new List<PerformanceCounter>();
        public virtual void InitializeCounters(Process p)
        {
            foreach (string counterName in Enum.GetNames(typeof(DBPerformanceCounters)))
            {
                PerformanceCounter PerfCounter = new PerformanceCounter();
                PerfCounter.CategoryName = ".NET Data Provider for SqlServer";
                PerfCounter.CounterName = counterName;
                PerfCounter.InstanceName = GetAssemblyName(p);
                lstPerfCounters.Add(PerfCounter);
            }
        }

        
        public List<ConnectionsViewItem> GetCounters()
        {
            var lst = new List<ConnectionsViewItem>();
            foreach (PerformanceCounter p in lstPerfCounters)
            {
                lst.Add(new ConnectionsViewItem(p.CounterName, p.NextValue()));
            }
            return lst;
        }

        public ConnectionsView ViewConnections(Process p)
        {
            InitializeCounters(p);
            return new ConnectionsView(GetCounters());
        }
    }

    /// <summary>
/// Represents the class public.
/// </summary>
public class ConnectionsViewItem
    {
        public string Name { get; set; }
        public float Value { get; set; }

        public ConnectionsViewItem(string name, float value)
        {
            Name = name;
            Value = value;
        }
    }

    public enum DBPerformanceCounters
    {
        NumberOfActiveConnectionPools,
        NumberOfActiveConnections,
        NumberOfFreeConnections,
        NumberOfNonPooledConnections,
        NumberOfPooledConnections,
        SoftDisconnectsPerSecond,
        SoftConnectsPerSecond,
        NumberOfReclaimedConnections,
        HardConnectsPerSecond,
        HardDisconnectsPerSecond,
        NumberOfActiveConnectionPoolGroups,
        NumberOfInactiveConnectionPoolGroups,
        NumberOfInactiveConnectionPools,
        NumberOfStasisConnections
    }
}
