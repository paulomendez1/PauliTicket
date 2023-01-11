import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'PauliTicket.UI';
  isSticky: boolean = false;

  @HostListener('window:scroll', ['$event'])
  checkScroll() {
    this.isSticky = window.pageYOffset >= 70;
  }

  constructor(public authService: AuthService, private router: Router) { }

  moveToProfile() {
    this.router.navigate([`profile/${this.authService.getFieldFromJWT('uid')}`]);
  }
}
