import { ChangeDetectionStrategy, Component, inject, PLATFORM_ID } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { ProductService } from '../../services/product.service';
import { lastValueFrom } from 'rxjs';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { isPlatformBrowser } from '@angular/common';


@Component({
  selector: 'app-product',
  imports: [MatCardModule, MatButtonModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss',
})
export class ProductComponent {
  private productService = inject(ProductService);
  private authService = inject(AuthService);
    private platformId = inject(PLATFORM_ID);


  ngOnInit() {
    this.initailize();
  }

  async initailize() {
    if(isPlatformBrowser(this.platformId))
    await this.callAPI();
  }

  callAPI = async () => {
    const products = await lastValueFrom(
      this.productService.getProducts('20.63799', '93.1637', 1, 5)
    );
    console.log(products);
  };

  async onGetDataClick(){
    await this.callAPI();
  }

  onLogOutClick(){
    this.authService.logout();
  }
}
