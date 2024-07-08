import 'bootstrap/dist/css/bootstrap.css';
import { useEffect, useState } from "react";



const Searcher = () => {
    const [flights, setFlights] = useState([]);

    const ConsumeAPI = async () => {
        const response = await fetch("api/flights");  
        if (response.ok) {  
            const data = await response.json();  
            console.log(data);  
            setFlights(data);  
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