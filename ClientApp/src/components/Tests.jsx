import 'bootstrap/dist/css/bootstrap.css';
import { useEffect, useState } from "react";



const Searcher = () => {
    const [route, setRoute] = useState([]);


    const ConsumeAPI = async () => {
            try {
                const response = await fetch(`/api/flights/${2}_${19}`);
                if (response.ok) {

                    const data = await response.json();

                    setRoute(data)

                } else {
                    throw new Error('Error en la solicitud');
                }
            } catch (error) {
                console.error('Fetch error:', error);
            }
    };

        
    
    useEffect(() => {
        ConsumeAPI()

    }, []);

    return (

        <div>
            {route.map((item) => (
                <li key={item.id} value={item.id}>{item.id} - {item.country} - {item.city}</li>

            ))
            }
        </div>
        )

}

export default Searcher;