import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { Router, NavigationEnd } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'lib-header',
  templateUrl: './header.component.html',
  styles: [],
})

export class HeaderComponent implements OnInit, OnDestroy {
  currentUser: any;
  currentUserSubscription: Subscription | undefined;
  activeLink: string = '';
  constructor(private authService: AuthService, private router: Router) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.setActiveLink(event.url);
      }
    });
  }

  setActiveLink(url: string): void {
    if (url === '/trip/new') {
      this.activeLink = '/trip/new';
    } else if (url === '/trip') {
      this.activeLink = '/trip';
    } else {
      this.activeLink = '';
    }
  }

  ngOnInit(): void {
    this.currentUserSubscription = this.authService.currentUser$.subscribe(
      (user) => {
        this.currentUser = user ? user.name : null;
      }
    );
    this.loadCurrentUser();
  }

  ngOnDestroy(): void {
    if (this.currentUserSubscription) {
      this.currentUserSubscription.unsubscribe();
    }
  }

  loadCurrentUser(): void {
    if (this.authService.isLoggedIn()) {
      this.authService.loadCurrentUser();
    }
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  logout(): void {
    this.authService.logout();
    this.currentUser = null;
  }
}