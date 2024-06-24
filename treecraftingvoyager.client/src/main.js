import './assets/main.css';

import { createApp } from 'vue';
import App from './App.vue';
import router from '@/router';
import store from '@/store';
import 'bootstrap';
import i18n from '@/localization';
import { Field, Form, ErrorMessage } from '@/validation';

const app = createApp(App);

app.component('Field', Field);
app.component('Form', Form);
app.component('ErrorMessage', ErrorMessage);

app.use(router);
app.use(store);
app.use(i18n);

app.mount('#app');
