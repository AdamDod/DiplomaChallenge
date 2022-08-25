import { Component } from '@angular/core';
import { Customer } from './models/customer';
import { Order } from './models/order';
import { Product } from './models/Product';
import { OrdersService } from './services/orders.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'front-end';
  orders:Order[] = [];
  customers:Customer[] = [];
  products:Product[] = [];
  newOrder:Order;
  selectedCustomer:Customer;
  selectedProduct:Product;
  selectedQty:number = 0;
  selectedShipMode:string;
  selectedOrderToDelete:Order;
  selectedOrderToEdit:Order;
  editedOrder:Order;
  loaded:boolean = false;


  constructor(public _orderService:OrdersService){
    this.load();
  }

  load(){
    this._orderService.GetOrders().subscribe(unpackedOrders => this.orders = unpackedOrders, null, ()=>{
      this._orderService.GetCustomers().subscribe(unpackedCustomers => this.customers = unpackedCustomers, null, ()=>{
        this._orderService.GetProducts().subscribe(unpackedProducts => this.products = unpackedProducts, null, ()=>{
          this.loaded = true;
          this.selectedOrderToEdit = this.orders[0];
          this.editedOrder = {...this.selectedOrderToEdit};
        });
      });
    });
    
    
  }

  addOrder(){
    this.newOrder = {
      custID:this.selectedCustomer.custID,
      prod:this.selectedProduct,
      orderDate:new Date().toString(),
      shipDate:"",
      quantity:this.selectedQty,
      shipMode:this.selectedShipMode
    }
    this._orderService.newOrder(this.newOrder).subscribe(null,null,()=>{
      alert("Order has been placed");
      this.load();
    })
  }
  
  deleteOrder(){
    this._orderService.deleteOrder(this.selectedOrderToDelete).subscribe(null,null,()=>{
      alert("Order has been deleted");
      this.load();
    })
  }

  editOrder(){
    this._orderService.deleteOrder(this.selectedOrderToEdit).subscribe(null,null,()=>{
      this._orderService.newOrder(this.editedOrder).subscribe(null,null,()=>{
        alert("order changed")
        this.load();
      })
    })
  }

  changedEdit(){
    this.editedOrder = {...this.selectedOrderToEdit};
  }
}
