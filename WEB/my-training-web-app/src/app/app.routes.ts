import { Routes } from '@angular/router';
import { LoginComponent } from './shared/components/login/login.component';
import { ProductComponent } from './features/products/components/product/product.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'product', component: ProductComponent },
];
