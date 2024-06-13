using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SmartTime.Models
{
    public class DailyStat
    {
        [JsonProperty("data")]
        List<ProjectData> projects { get; set; }

        public float GetAllValueTimeInProjects()
        {
            if (projects == null)
                return float.NaN;
            float sum = default;
            foreach (ProjectData data in projects)
            {
                sum += data.Duration;
            }
            return sum;
        }
    }
    class Data
    {
        List<ProjectData> projects { get; set; }

        public float GetAllTimeInProjects()
        {
            float sum = default;
            foreach (ProjectData data in projects)
            {
                sum +=data.Duration;
            }
            return sum;
        }
    }
    class ProjectData
    {
        [JsonProperty("project")]
        public string Name { get; set; }
        [JsonProperty("duration")]
        public float Duration { get; set; }
    }
}
