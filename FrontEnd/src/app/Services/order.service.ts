import { Injectable } from '@angular/core';

import{HttpClient} from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Order } from '../Interfaces/order';
import { OrderList } from '../Interfaces/order-list';
import { OrderCreate } from '../Interfaces/order-create';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private endpoint:string = environment.endPoint;
  private apiUrl:string = this.endpoint + "Order/"
  
  constructor(private http:HttpClient) { }

  get():Observable<Order[]>{
    return this.http.get<Order[]>(`${this.apiUrl}Get`);
  }

  getPrediction():Observable<OrderList[]>{
    return this.http.get<OrderList[]>(`${this.apiUrl}GetPrediction`);
  }

  create(model:OrderCreate):Observable<OrderCreate>{
    return this.http.post<OrderCreate>(`${this.apiUrl}Post`, model);
  }  
}
