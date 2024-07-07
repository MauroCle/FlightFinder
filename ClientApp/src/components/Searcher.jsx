import 'bootstrap/dist/css/bootstrap.css';
import { useEffect, useState } from "react";



const Searcher = () => {
    const [flights, setFlights] = useState([]);

    const ConsumeAPI = async () => {
        const response = await fetch("api/flights");  // Making a fetch request to the specified API endpoint
        if (response.ok) {  // Checking if the response status is in the range 200-299 (successful response)
            const data = await response.json();  // Parsing the JSON data from the response
            console.log(data);  // Logging the fetched data to the console
            setFlights(data);  // Updating the 'flights' state with the fetched data
        }
    }
    useEffect(() => {
        ConsumeAPI()
    }, []);

    return (
        <div className="bg-success">
            {
            flights.map((item) => (
                <div key={item.id}>
                    <li>{item.originId}</li>
                    <li>{item.destinationId}</li>
                    <li>{item.departureDate}</li>
                </div>
            ))
            }
        </div>
    )
}

export default Searcher;