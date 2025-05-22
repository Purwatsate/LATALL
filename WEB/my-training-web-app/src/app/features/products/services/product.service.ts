import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private http = inject(HttpClient);
  private baseUrl = '';

  constructor() {
    this.baseUrl = environment.apiBaseUrl;
  }

  getProducts(
    latitude: string,
    longitude: string,
    pageNumber: number,
    pageSize: number
  ): Observable<Product> {
    const params = {
      latitude: latitude,
      longitude: longitude,
      pageNumber: pageNumber,
      pageSize: pageSize,
    };
    return this.http.get<Product>(`${this.baseUrl}/product/Products`, {
      params,
    });
  }
}
