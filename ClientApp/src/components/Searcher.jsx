import 'bootstrap/dist/css/bootstrap.css';
import { useEffect, useState } from "react";



const Searcher = () => {
    const [origins, setOrigins] = useState([]);
    const [departures, setDepartures] = useState([]);
    const [SelectedOrigin, setSelectedOrigin] = useState([]);
    const [SelectedDepartures, setSelectedDepartures] = useState([]);

    const ConsumeAPI = async () => {
        const response = await fetch("/api/airports");  
        if (response.ok) {  
            const data = await response.json();  
            //console.log(data);  
            setOrigins(data);  
            setDepartures(data);
        }
    }
    useEffect(() => {
        ConsumeAPI()

    }, []);



    const GetAirportlinkedWith = async (id,origin) => {
        try {
            const response = await fetch(`/api/airports/LinkedWith/${id}_${origin}`);
            if (response.ok) {

                const data = await response.json();

                origin ? setDepartures(data) : setOrigins(data);

            } else { 
                ConsumeAPI();
            throw new Error('Error en la solicitud');
        }
        } catch (error) {
            console.error('Fetch error:', error);
        }
    };


    return (
        <div className="container mt-5">
            <form>
                <div className="row form-row align-items-end">
                    <div className="form-group col-md-2">
                        <label for="tripType">Trip Type</label>
                        <div className="form-check">
                            <input className="form-check-input" type="radio" name="tripType" id="roundTrip" value="roundTrip" checked/>
                                <label className="form-check-label" for="roundTrip">
                                    Round Trip
                                </label>
                        </div>
                        <div className="form-check">
                            <input className="form-check-input" type="radio" name="tripType" id="oneWay" value="oneWay"/>
                                <label className="form-check-label" for="oneWay">
                                    One Way
                                </label>
                        </div>
                    </div>
                    <div className="form-group col-md-2">
                        <label for="passengers">Departure</label>
                        <select id="origin" className="form-control" onChange={(e) => GetAirportlinkedWith(e.target.value, true)}>
                            <option value=""></option>

                            {origins.map((item) => (                         
                                <option key={item.id} value={item.id}>{item.country} - {item.city}</option>

                            ))
                            }

                        </select>
                    </div>
                    <div className="form-group col-md-2">
                        <label for="passengers">Destination</label>
                        <select className="form-control" id="destination" onChange={(e) => GetAirportlinkedWith(e.target.value, false)}>
                            <option></option>

                            {departures.map((item) => (
                                <option key={item.id} value={item.id}>{item.country} - {item.city}</option>
                            ))}

                        </select>

                    </div>
                    <div className="form-group col-md-2">
                        <label for="departureDate">Departure Date</label>
                        <input type="text" className="form-control datepicker" id="departureDate" placeholder="dd/mm/yyyy"/>
                    </div>
                    <div className="form-group col-md-2">
                        <label for="returnDate">Return Date</label>
                        <input type="text" className="form-control datepicker" id="returnDate" placeholder="dd/mm/yyyy"/>
                    </div>
                    <div className="form-group col-md-2">
                        <label for="passengers">Passengers</label>
                        <select className="form-control" id="passengers">
                            <option>1</option>
                            <option>2</option>
                            <option>3</option>
                            <option>4</option>
                        </select>
                    </div>
                    <div className="form-group col-md-1">
                        <button type="submit" className="btn btn-primary btn-block">Search</button>
                    </div>
                </div>
            </form>
        </div>
    )
}

export default Searcher;