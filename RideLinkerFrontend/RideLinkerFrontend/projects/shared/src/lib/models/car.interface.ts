import { Location } from "./location.interface";

export interface Car {
    id: number;
    brand: string;
    model: string;
    image: string;
    location: Location;
    available: boolean;
}
