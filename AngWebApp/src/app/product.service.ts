import { Injectable } from '@angular/core';
import {Product} from './product';
import {PRODUCTS} from './mock-Products';

@Injectable({
  providedIn: 'root'
})

export class ProductService {

  getProducts():Product[]{
    return PRODUCTS;
  }
  constructor() { }
}
