import { Injectable } from '@angular/core';

import {HttpClient} from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Product } from '../Interfaces/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private endpoint:string = environment.endPoint;
  private apiUrl:string = this.endpoint + "Product/"
  
    constructor(private http:HttpClient) { }
  
    getList():Observable<Product[]>{
      return this.http.get<Product[]>(`${this.apiUrl}`);
    }
  }