import { createI18n } from 'vue-i18n';
import en from '@/locales/en.json';
import pl from '@/locales/pl.json';

const messages = {
    en,
    pl
};

const i18n = createI18n({
    locale: 'en',
    fallbackLocale: 'en',
    messages,
});

export default i18n;
