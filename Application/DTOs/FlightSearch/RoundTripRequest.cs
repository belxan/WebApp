namespace Application.DTOs.FlightSearch;

public class RoundTripRequest
{
    public string signature { get; set; }
    public string marker { get; set; }
    public string host { get; set; }
    public string user_ip { get; set; }
    public string locale { get; set; }
    public string trip_class { get; set; } = "Y";
    public PassengerInfo passengers { get; set; }
    public List<Segment> segments { get; set; }
    public string know_english { get; set; } = "true";
    public string currency { get; set; } = "USD";
}

public class PassengerInfo
{
    public int adults { get; set; }
    public int children { get; set; }
    public int infants { get; set; }
}

public class Segment
{
    public string origin { get; set; }
    public string destination { get; set; }
    public string date { get; set; } // yyyy-MM-dd
}
