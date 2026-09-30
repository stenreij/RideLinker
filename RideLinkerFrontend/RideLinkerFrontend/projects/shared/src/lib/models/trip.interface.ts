import { Car } from "./car.interface";
import { Location } from "./location.interface";
import { Reservation } from "./reservation.interface";
import { User } from "./user.interface";

export interface Trip {
    id: number;
    departure: Location;
    destination: Location;
    startTime: Date;  
    endTime: Date;
    driver: User;
    car: Car;
}
