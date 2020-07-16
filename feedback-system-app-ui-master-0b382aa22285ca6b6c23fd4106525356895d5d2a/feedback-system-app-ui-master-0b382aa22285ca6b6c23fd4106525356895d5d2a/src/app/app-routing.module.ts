import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_helpers/auth.guard';
import { EventsComponent } from './events/events.component';
import { ReportsComponent } from './reports/reports.component';
import { ConfigurationComponent } from './configuration/configuration.component';
import { FeedbackComponent } from './feedback/feedback.component';
import { UploadComponent } from './upload/upload.component';

const routes: Routes = [
   
    { path: 'login', component: LoginComponent },
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'events', component: EventsComponent, canActivate: [AuthGuard]},
    { path: 'reports', component: ReportsComponent, canActivate: [AuthGuard]},
    { path: 'configuration', component: ConfigurationComponent, canActivate: [AuthGuard]},
    { path: 'feedback', component: FeedbackComponent }, // ,  canActivate: [AuthGuard]
    { path: 'upload', component: UploadComponent, canActivate: [AuthGuard]},
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);
