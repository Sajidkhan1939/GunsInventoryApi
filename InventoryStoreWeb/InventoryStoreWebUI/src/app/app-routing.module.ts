import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignupComponent } from './user/signup/signup.component';

const routes: Routes = [{ path: 'user', loadChildren: () => import('./user/user.module').then(m => m.UserModule)},
  {
    path:'**',
    redirectTo:'user',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
