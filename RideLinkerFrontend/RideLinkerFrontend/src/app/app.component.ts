import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationEnd, Router, RouterModule, RouterOutlet } from '@angular/router';
import { SharedModule } from '@shared/src/lib/shared/shared.module';
import { CarModule } from '@car/src/public-api';
import { RideModule } from '@ride/src/lib/ride/ride.module';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, RouterOutlet, SharedModule, CarModule, RideModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  constructor(private router: Router,) {

  }
  ngOnInit() {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
          let htmlElement = document.getElementById('custom_js');
          if (htmlElement != null) {
            htmlElement.remove();
          }
          const node = document.createElement('script');
          node.src = 'assets/main.js';
          node.type = 'text/javascript';
          node.async = false;
          node.id = 'custom_js';
          node.charset = 'utf-8';
          document.getElementsByTagName('head')[0].appendChild(node);
      }
  });
  }
  title = 'rideLinker';
}
