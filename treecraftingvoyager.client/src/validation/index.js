import { configure, defineRule, Field, Form, ErrorMessage } from 'vee-validate';
import { required, email, min, confirmed } from '@vee-validate/rules';
import { localize } from '@vee-validate/i18n';
import en from '@vee-validate/i18n/dist/locale/en.json';
import pl from '@vee-validate/i18n/dist/locale/pl.json';

defineRule('required', required);
defineRule('email', email);
defineRule('min', min);
defineRule('confirmed', confirmed);

configure({
    generateMessage: localize({
        en,
        pl
    }),
    validateOnInput: true // Walidacja podczas wprowadzania danych
});

export { Field, Form, ErrorMessage };
