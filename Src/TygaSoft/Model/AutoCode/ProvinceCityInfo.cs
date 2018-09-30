using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class ProvinceCityInfo
    {
	    public object Id { get; set; }

        public string Named { get; set; } 

public string Pinyin { get; set; } 

public string FirstChar { get; set; } 

public Guid ParentId { get; set; } 

public Int32 Sort { get; set; } 

public string Remark { get; set; } 

public DateTime LastUpdatedDate { get; set; } 
    }
}
