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
                        { name: 'ProfilePicture', type: 'file', placeholder: '', data: '', required: '', readonly: false },
                        { name: 'Phone', type: 'number', placeholder: 'Phone, example: 1236549877', data: '', required: '', readonly: false },
                        { name: 'OptionalPhone', type: 'number', placeholder: 'House, Office or parents', data: '', required: '', readonly: false },
                        { name: 'Password', type: 'password', placeholder: 'Password', data: '', required: 'required|', readonly: false },
                        { name: 'ConfirmPassword', type: 'password', placeholder: 'ConfirmPassword', data: '', required: 'required|', readonly: false },
                        { name: 'Address', type: 'text', placeholder: 'Address', data: '', required: 'required|', readonly: false },
                        { name: 'State', type: 'text', placeholder: '', data: 1, required: 'required|', readonly: true },
                        { name: "Account type", type: 'select', data: null, class: "", options: [ 
                                { name: 'Client', placeholder: '', required: 'required|', readonly: false },
                                { name: 'Business', placeholder: '', required: 'required|', readonly: false }
                            ] ,
                        }
                        
                    ],
                    formButtons: [
                        { name: 'Cancel', class: "btn btn-danger btn-block", action: this.cancelRegistration },
                        { name: 'Register', class: "btn btn-success btn-block", action: this.register } 
                    ],
                    page: {
                        title: 'Register',
                        isLoading: false
                    }
                },
                formButtons: []
            }
        },
        components: {
            'ep-form': EPForm
        },

        created() {},

        computed: {

        },
 
        methods: {
            register(userData) {
                if (this.checkPasswordEquality(userData) && this.checkPasswordLength(userData)) {
                    if (confirm("¿Are you sure you want to signup with this information?")) {
                        this.$http.post(`${api.url}user`, userData)
                        .then(response => {
                            alert(`User ${userData.Name} ${userData.Lastname} successfully created, thank you for joining us!!`);
                            this.$store.commit('login', response.data[0]);
                            this.$router.replace('/');
                        })
                        .catch(err => this.displayAlert(err));
                    }
                } else {
                    alert('Make sure both passwords are equal');
                }
            },

            cancelRegistration() {
                if (confirm('You are about to leave this page, are you sure?')) {
                    this.$router.replace('/');
                }
            },

            checkPasswordLength(userData) {
                return userData.Password.length;
            },

            checkPasswordEquality(userData) {
                return userData.Password === userData.ConfirmPassword ? true : false;
            }
        }
    }
</script>
