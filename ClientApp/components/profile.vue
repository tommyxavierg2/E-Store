<template>
    <div>
        <div class="jumbotron">
            <h1>{{records.page.title}}</h1>
        </div>

        <div class="container" style="border: 1px solid gray;">

            <div class="row" style="border-bottom: 1px solid gray; padding: 25px;">
                <div class="col-md-6">
                    <div class="card user">
                        <img class="card-img-top" src="https://firebasestorage.googleapis.com/v0/b/morse-code-7eb28.appspot.com/o/robot.png?alt=media&token=c58acb41-2199-4a8d-a018-6514ecd37547" alt="UserPhoto">
                        <div class="card-block">
                            <h3>{{fullName}}</h3>                    
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <p :class="{ 'control': true }">
                            <label>Name</label>
                            <input name="Name" type="text" class="form-control" v-model="records.user.Name" :readonly="!records.page.editing" v-validate="'required|'">
                            <span class="text-danger" v-show="errors.has(records.user.Name)">{{ errors.first(records.user.Name) }}</span>
                        </p>
                        <p :class="{ 'control': true }">
                            <label>Lastname</label>
                            <input name="Lastname" type="text" class="form-control" v-model="records.user.Lastname" :readonly="!records.page.editing" v-validate="'required|'">
                            <span class="text-danger" v-show="errors.has(records.user.Lastname)">{{ errors.first(records.user.Lastname) }}</span>
                        </p>
                        <p :class="{ 'control': true }">
                            <label>Email</label>
                            <input name="Email" type="text" class="form-control" v-model="records.user.Email" :readonly="!records.page.editing" v-validate="'required|email'">
                            <span class="text-danger" v-show="errors.has(records.user.Email)">{{ errors.first(records.user.Email) }}</span>
                        </p>   
                        <p :class="{ 'control': true }">
                            <label>Phone</label>
                            <input name="Phone" type="text" class="form-control" v-model="records.user.Phone" :readonly="!records.page.editing">
                            <span class="text-danger" v-show="errors.has(records.user.Phone)">{{ errors.first(records.user.Phone) }}</span>
                        </p>
                        <p :class="{ 'control': true }">
                            <label>Optional Phone</label>
                            <input name="OptionalPhone" type="text" class="form-control" v-model="records.user.OptionalPhone" :readonly="!records.page.editing">
                            <span class="text-danger" v-show="errors.has(records.user.OptionalPhone)">{{ errors.first(records.user.OptionalPhone) }}</span>
                        </p>
                        <p :class="{ 'control': true }">
                            <label>Address</label>
                            <input type="text" class="form-control" v-model="records.user.Address" :readonly="!records.page.editing" v-validate="'required|'">
                        </p>
                    </div>
                    <button class="btn btn-danger btn-block" @click="records.page.editing = true" v-if="!records.page.editing">Edit</button>
                    <div class="row" v-if="records.page.editing">
                        <div class="col-md-6">
                            <button class="btn btn-danger btn-block" @click="cancelEditing">Cancel</button>
                        </div>
                        <div class="col-md-6">
                            <button class="btn btn-success btn-block" @click="updateUser(records.user)">Update</button>
                        </div> 
                    </div>
                </div>
            </div>

            <div class="row" style="margin-bottom: 25px; padding: 25px;">
                <div class="col-md-4">
                    <button class="btn">
                        <icon icon="list"/> Orders
                    </button>
                </div>
                <div class="col-md-4">
                    <button class="btn">
                        <icon icon="credit-card"/> Payment Information
                    </button>
                </div>
                <div class="col-md-4" v-if="records.user.Business">
                    <button class="btn">
                        <icon icon="list"/> Inventory
                    </button>
                </div>
            </div>

        </div>

    </div>
</template>

<script>
    import {api} from '../store/api';   
    import epform from './epform-template/epform';
    import  VeeValidate from 'vee-validate';
    import store from '../store/index';

    export default {
        data() {
            return {
                records: {
                    page: { title: 'Profile', editing: false },
                    user: {},
                    originalUser: {},
                    tabsInfo: [],
                    tabsContent: [],
                    tabsContent1: []
                }
            }
        },
        components: {
            'ep-form': epform
        },
        computed:{
            fullName() {
                return `${this.records.user.Name} ${this.records.user.Lastname}`; 
            }
        },
        methods: {
            getUser(id) {
                this.$http.get(`${api.url}users?id=${id}`)
                .then(response => {
                    this.records.user = response.data[0];
                    this.records.originalUser = JSON.stringify(this.records.user);
                })
                .catch(err => toastr.error(err))
            },

            updateUser(userData) {
                this.$validator.validateAll().then((result) => {
                    if (result) {
                        this.$http.put(`${api.url}users/${userData.id}`, userData)
                            .then(response => {
                                this.records.originalUser = JSON.stringify(this.records.user);
                                toastr.success('User sucessfuly update');
                                this.records.page.editing = false;
                            })
                            .catch(err => toastr.error(err));
                    } else {
                        toastr.warning('Make sure all fields are properly filled');
                    }
                });

            },

            cancelEditing() {
                this.records.user = JSON.parse(this.records.originalUser);
                this.records.page.editing = false;
            } 
        },
        created() {
            this.getUser(this.$route.params.id);
        }
    }
</script>

<style>
    .user {
        width: 50%;
    }
</style>
