<template>
    <div>

        <form>

            <h1 class="jumbotron titles">Login</h1>

            <ep-form :formFields="formFields" :buttons="formButtons"></ep-form>

        </form>

    </div>
</template>

<script>
    import EPForm from './epform-template/epform';
    import {api} from '../store/api';
    import store from './../store/index';

    export default {
        data() {
            return {
                formFields: [],
                formButtons: []
            } 
        },
        components: {
            'ep-form': EPForm
        },
        
        computed: {
            checkLoggedIn() {
                this.$store.state.loggedIn && this.$router.replace('/'); 
            }
        },

        created() {
            this.formFields.push(
                { name: 'Email', type: 'email', placeholder: 'Email@example.com', data: '', required: 'required', readonly: false },
                { name: 'Password', type: 'password', placeholder: 'Password', data: '', required: 'required', readonly: false },
                { name: 'Remember', type: 'checkbox', class: "input", placeholder: '', data: false, required: '', readonly: false }
            );
            this.formButtons.push(
                { name: 'Login',  class: "btn btn-success btn-block", action: this.login }
            );
        },

        mounted() {
        },

        methods: {
            login(userData) {
                this.$http.get(`${api.url}user?email=${userData.Email}&password=${userData.Password}`)
                .then(response => {
                    if (response.data.length) {
                        this.$store.commit('login', response.data[0]);
                        localStorage.setItem('userData', JSON.stringify(response.data[0]));
                        this.$router.push('/');
                    } else {
                        alert('NOPE!!');
                    }
                })
                .catch(err => alert(err))
             }

        }

    }

</script>

<style lang="css" src="">
</style>