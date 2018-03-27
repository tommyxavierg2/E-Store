<template>
    <div class="align-center">

        <h1 class="jumbotron titles">{{records.page.title}}</h1>

        <ep-form :formFields="records.formFields" :buttons="records.formButtons"></ep-form>

    </div>
</template>

<script>
    import { api } from '../store/api';
    import EPForm from './epform-template/epform';

    export default {
        data() {
            return {
                records: {
                    formFields: [
                        { name: 'Name', type: 'text', placeholder: 'Name', data: '', required: 'required|', readonly: false },
                        { name: 'Lastname', type: 'text', placeholder: 'LastName', data: '', required: 'required|', readonly: false },
                        { name: 'Email', type: 'email', placeholder: 'Email@example.com', data: '', required: 'required|email', readonly: false },
                        { name: 'Phone', type: 'number', placeholder: 'Phone, example: 1236549877', data: '', required: '', readonly: false },
                        { name: 'OptionalPhone', type: 'number', placeholder: 'House, Office or parents', data: '', required: '', readonly: false },
                        { name: 'Password', type: 'password', placeholder: 'Password', data: '', required: 'required|', readonly: false },
                        { name: 'ConfirmPassword', type: 'password', placeholder: 'ConfirmPassword', data: '', required: 'required|', readonly: false },
                        { name: 'Address', type: 'text', placeholder: 'Address', data: '', required: 'required|', readonly: false },
                        { name: 'Client', type: 'checkbox', class: "input", placeholder: '', data: false, required: '', readonly: false },
                        { name: 'Business', type: 'checkbox', class: "input", placeholder: '', data: false, required: '', readonly: false }
                    ],
                    formButtons: [
                        { name: 'Cancel', class: "btn btn-danger btn-block", action: this.cancelRegistration },
                        { name: 'Register', class: "btn btn-success btn-block", action: this.register } 
                    ],
                    page: {
                        title: 'Register'
                    }
                },
                formButtons: []
            }
        },
        components: {
            'ep-form': EPForm
        },

        created() {},

        methods: {
            register(userData) {
                if (userData.Password === userData.ConfirmPassword) {
                        this.$http.post(`${api.url}users`, userData)
                        .then(response => {
                            toastr.success(`User ${userData.name} ${userData.lastname} successfully created, thank you for joining us!!`);
                            this.$store.commit('login', response.data);
                            this.$router.replace('/');
                        })
                        .catch(err => this.displayAlert(err));
                } else {
                    toastr.warning('Make sure both passwords are equal');
                }
            },

            cancelRegistration() {
                if (confirm('You are about to leave this page, are you sure?')) {
                    this.$router.replace('/');
                }
            }
        }
    }
</script>
