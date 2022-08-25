import { Product } from "./Product";

export interface Order {
    orderDate:string;
    quantity:number;
    shipDate:string;
    shipMode:string;
    prod:Product;
    custID:string;
}
