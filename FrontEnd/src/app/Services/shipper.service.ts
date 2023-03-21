import { Injectable } from '@angular/core';

import {HttpClient} from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Shipper } from '../Interfaces/shipper';

@Injectable({
  providedIn: 'root'
})
export class ShipperService {
  private endpoint:string = environment.endPoint;
  private apiUrl:string = this.endpoint + "Shipper/"
  
    constructor(private http:HttpClient) { }
  
    getList():Observable<Shipper[]>{
      return this.http.get<Shipper[]>(`${this.apiUrl}`);
    }
  }
