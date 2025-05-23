import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, of, tap } from 'rxjs';
import { Product } from '../models/product.model';
import { environment } from '../../../../environments/environment';
import { TransferState, makeStateKey } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private http = inject(HttpClient);
  private baseUrl = '';

  PRODUCT_KEY = makeStateKey<any>('products');
  private state = inject(TransferState);

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

    const saved = this.state.get(this.PRODUCT_KEY, null);
    if (saved) {
      return of(saved);
    }

    return this.http
      .get<Product>(`${this.baseUrl}/product/Products`, {
        params,
      })
      .pipe(tap((data) => this.state.set(this.PRODUCT_KEY, data)));
  }
}
