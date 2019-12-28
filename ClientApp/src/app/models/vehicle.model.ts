export interface KeyValuePair {
    id: number;
    name: string;
}
export interface Contact {
    Name: string;
    Phone: string;
    Email: string;
}
export interface Vehicle {
    id: number;
    model: KeyValuePair;
    make: KeyValuePair;
    Registered: boolean;
    vehicleFeatures: KeyValuePair[];
    contact: Contact;
    lastUpdate: string;
}
export interface SaveVehicle {
    id: number;
    modelId: number;
    makeId: number;
    isRegistered: boolean;
    vehicleFeatures: number[];
    Contact: Contact;
}
export interface SendVehicle {
    modelId: number;
    isRegistered: boolean;
    vehicleFeatures: number[];
    Contact: Contact;
}
export interface QueryResult {
    totalItems;
    items:any [];
}
export interface Photo {
    items:any [];
}
