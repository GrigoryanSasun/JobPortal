import angular from 'angular';
import { CommonModule } from './common/common.module';
import { AppComponent } from './app.component';

export const AppModule = angular
  .module('app', [
    'ngAnimate',
    'ui.router',
    'ui.bootstrap',
    CommonModule
  ])
  .component('app', AppComponent)
  .name;
