import { useEffect, useState } from "react"
import { CarRentalOffer } from "../../components/CarRentalOffer/CarRentalOffer"


interface CarOffer {
    brand: string,
    model: string,
    details: Array<CarDetails>,
    id: number
}


export interface CarDetails {
    renter: string,
    capacity: number,
    enginePower: string,
    yearOfProduction: string,
    description: string
}

export const OffersPage: React.FC = () => {

    const [carOffers, setCarOffers] = useState(new Array<CarOffer>());
    const [offersExpanded, setOffersExpanded] = useState(new Array<boolean>());

    const handleOffersExpandedChange = (newValue: boolean, index: number) => {
        let items = [...offersExpanded];
        items[index] = newValue;
        setOffersExpanded(items);
    }


    // TODO: fetch car offer from api
    useEffect(() => {
        setCarOffers([{
            brand: "Volkswagen",
            model: "Polo",
            details: [
                { renter: "Wypożyczalnia BestCars", capacity: 5, enginePower: "130 HP", yearOfProduction: "2017", description: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur ducimus numquam dolores fugiat assumenda, explicabo repudiandae mollitia vero dolorem officiis cupiditate tempora, iste incidunt consequuntur enim officia soluta illum at." },
                { renter: "Wypożyczalnia Marek-rent", capacity: 5, enginePower: "130 HP", yearOfProduction: "2017", description: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur ducimus numquam dolores fugiat assumenda, explicabo repudiandae mollitia vero dolorem officiis cupiditate tempora, iste incidunt consequuntur enim officia soluta illum at." }],
            id: 0
        },
        {
            brand: "Audi",
            model: "A4",
            details: [
                { renter: "Wypożyczalnia BestCars2", capacity: 5, enginePower: "130 HP", yearOfProduction: "2017", description: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur ducimus numquam dolores fugiat assumenda, explicabo repudiandae mollitia vero dolorem officiis cupiditate tempora, iste incidunt consequuntur enim officia soluta illum at." },
                { renter: "Wypożyczalnia Marek-rent2", capacity: 5, enginePower: "130 HP", yearOfProduction: "2017", description: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur ducimus numquam dolores fugiat assumenda, explicabo repudiandae mollitia vero dolorem officiis cupiditate tempora, iste incidunt consequuntur enim officia soluta illum at." }],
            id: 1
        }]);
        setOffersExpanded([false, false]);
    }, []);

    return (
        <div className="space-y-5 px-52 py-10">
            {carOffers.map(c => <CarRentalOffer model={c.model} brand={c.brand} expanded={offersExpanded[c.id]}
                expandedChanged={(newValue) => handleOffersExpandedChange(newValue, c.id)} carsDetails={c.details}></CarRentalOffer>)
            }
        </div >
    )
}