import { CategoryDTO } from "./category";

export interface EventDTO {
    eventId: string;
    name: string;
    price: number;
    artist: string;
    date: Date;
    description: string;
    imageUrl: string;
    category: CategoryDTO
}