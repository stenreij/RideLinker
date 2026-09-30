import { Trip } from "./trip.interface";
import { User } from "./user.interface";

export interface Reservation {
    id: number;
    userId: number;
    user: User;
    trip: Trip;
    tripId: number;
}
