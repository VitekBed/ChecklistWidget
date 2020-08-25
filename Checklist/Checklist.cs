using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VitekWidget.Checklist
{
    public class Checklist
    {
        public string Title { get; private set; }
        public ChecklistType Type { get; private set; }
        [JsonIgnore]
        public bool IsComplete { get { return Items.All(x => x.IsComplete); } }
        [ImmutableObject(true)]
        public ImmutableList<ChecklistItem> Items { get { return ImmutableList<ChecklistItem>.Empty.AddRange(items); } }
        private readonly IEnumerable<ChecklistItem> items;
        public void Reset()
        {

        }
        public Checklist Clone()
        {
            throw new NotImplementedException();
        }
        public DateTime? CompletedTime { get { return this.Items.Where(x => x.IsComplete).Max(x => x.CompletedTime); } }

        public Checklist(string title, ChecklistType type, IEnumerable<ChecklistItem> items)
        {
            this.Title = title;
            this.Type = type;
            this.items = items;
        }
        public static Checklist Deserialize(string json)
        {
            return JsonSerializer.Deserialize<Checklist>(json);
        }
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

    }

    public enum ChecklistType
    {
        Progressive,
        Regressive
    }
    [Serializable]
    public class ChecklistItem
    {
        public string Caption { get; }
        [JsonIgnore]
        public bool IsComplete { get; private set; }
        public void SetComplete()
        {
            this.IsComplete = true;
            this.CompletedTime = DateTime.Now;
        }
        public DateTime? CompletedTime { get; private set; }
        public string Note { get; set; }
        public void Reset()
        {
            this.IsComplete = false;
            this.CompletedTime = null;
        }
    }
}
