import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, retry, timeout } from 'rxjs';
import { environment } from 'src/enviroments/environment';
import { OrderDTO } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  ORDER_URL = environment.apiURL + '/Order';

  constructor(private http: HttpClient) { }

  public createOrder(order: OrderDTO): Observable<string> {
    const headers = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
    }
    return this.http.post<string>(this.ORDER_URL, order, { headers: headers });
  }

  public getOrder(orderId: string): Observable<OrderDTO> {
    const headers = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Access-Control-Expose-Headers': '*'
    }
    return this.http.get<any>(`${this.ORDER_URL}/${orderId}`, { headers: headers }).pipe(
      retry(3),
      timeout(15000),
      map(data => data as OrderDTO)
    );
  }

  public getPagedOrders(id: string, page: number, size: number): Observable<any> {
    const headers = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Access-Control-Expose-Headers': '*'
    }
    const params = new HttpParams()
      .set('id', id)
      .set('page', page)
      .set('size', size)
    return this.http.get<any>(`${this.ORDER_URL}/getpagedordersformonth`, { headers: headers, params: params }).pipe(
      retry(3),
      timeout(15000),
      map(data => data)
    );
  }
}
