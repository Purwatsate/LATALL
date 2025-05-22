import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { inject } from '@angular/core';
import { ProductService } from './features/products/services/product.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MatButtonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  private productService = inject(ProductService);
  isLoading = signal(false);

  ngOnInit() {}

  callAPI() {
    this.isLoading.set(true);
    setTimeout(() => {
      this.productService.getProducts('20.63799', '93.1637', 1, 5).subscribe({
        next: (data) => {
          this.isLoading.set(false);
          console.log(data);
        },
        error: (err) => {
          console.log(err);
        },
      });
    }, 4000);
  }
}
