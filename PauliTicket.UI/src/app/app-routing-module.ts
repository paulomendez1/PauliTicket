import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventDetailComponent } from './components/event-detail/event-detail.component';
import { HomeComponent } from './components/home/home.component';
import { OrderDetailComponent } from './components/order-detail/order-detail.component';
import { ProfileComponent } from './components/profile/profile.component';
import { LoginComponent } from './components/security/login/login.component';
import { RegisterComponent } from './components/security/register/register.component';
import { RequireAuthenticatedUserRouteGuard } from './utilities/auth-guard/require.guard';
import { RequireSameUserAsRequestRouteGuard } from './utilities/same-user-guard/same-user.guard';

const routes: Routes = [
  { path: 'home', component: HomeComponent, data: { title: 'Home' } },
  { path: 'login', component: LoginComponent, data: { title: 'Log In' } },
  { path: 'register', component: RegisterComponent, data: { title: 'Register' } },
  { path: 'event/:id', component: EventDetailComponent, data: { title: 'Event' }, canActivate: [RequireAuthenticatedUserRouteGuard] },
  { path: 'order/:id', component: OrderDetailComponent, data: { title: 'Order' }, canActivate: [RequireAuthenticatedUserRouteGuard] },
  { path: 'profile/:id', component: ProfileComponent, data: { title: 'Profile' }, canActivate: [RequireSameUserAsRequestRouteGuard] },

  { path: '**', redirectTo: 'home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }