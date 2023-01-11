export interface OrderDTO {
    userId: string;
    orderTotal: number;
    orderPlaced: Date
    orderPaid: boolean;
    eventId: string;
}