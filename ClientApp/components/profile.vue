<template>
    <div class="container text-center">
        
        <h1 class="jumbotron">{{records.page.title}}</h1>

        <b-card no-body>
            <b-tabs card v-model="records.page.tabIndex">
                <b-tab id="Personal" title="Personal" active>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="card">
                                <label for="profilePicture">
                                    <img class="card-img-top" :src="records.user.profilePicture" alt="UserPhoto">
                                </label>
                                <input id="profilePicture" type="file" style="display: none" accept="image/*" @change="addUserProfileImage($event)">
                                <div class="card-block">
                                    <h3>{{fullName}}</h3>                    
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <p :class="{ 'control': true }">
                                    <label>Name</label>
                                    <input name="Name" type="text" class="form-control" v-model.trim="records.user.name" :readonly="!records.page.editing" v-validate="'required|'">
                                    <span class="text-danger" v-show="errors.has(records.user.name)">{{ errors.first(records.user.name) }}</span>
                                </p>
                                <p :class="{ 'control': true }">
                                    <label>Lastname</label>
                                    <input name="Lastname" type="text" class="form-control" v-model.trim="records.user.lastName" :readonly="!records.page.editing" v-validate="'required|'">
                                    <span class="text-danger" v-show="errors.has(records.user.lastName)">{{ errors.first(records.user.lastName) }}</span>
                                </p>
                                <p :class="{ 'control': true }">
                                    <label>Email</label>
                                    <input name="Email" type="text" class="form-control" v-model.trim="records.user.email" :readonly="!records.page.editing" v-validate="'required|email'">
                                    <span class="text-danger" v-show="errors.has(records.user.email)">{{ errors.first(records.user.email) }}</span>
                                </p>   
                                <p :class="{ 'control': true }">
                                    <label>Phone</label>
                                    <input name="Phone" type="text" class="form-control" v-model.trim="records.user.phone" :readonly="!records.page.editing">
                                    <span class="text-danger" v-show="errors.has(records.user.phone)">{{ errors.first(records.user.phone) }}</span>
                                </p>
                                <p :class="{ 'control': true }">
                                    <label>Optional Phone</label>
                                    <input name="OptionalPhone" type="text" class="form-control" v-model.trim="records.user.optionalPhone" :readonly="!records.page.editing">
                                    <span class="text-danger" v-show="errors.has(records.user.optionalPhone)">{{ errors.first(records.user.optionalPhone) }}</span>
                                </p>
                                <p :class="{ 'control': true }">
                                    <label>Address</label>
                                    <input type="text" class="form-control" v-model.trim="records.user.address" :readonly="!records.page.editing" v-validate="'required|'">
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
                </b-tab>
                <b-tab v-if="!checkUserLogged.user.accountType" title="Cart">
                    <div v-if="checkCartState">
                        <h1 style="margin-bottom: 15px;">Your Cart</h1>
                        <product v-for="(item, index) in records.cartItems.products" :key="item.id" :product="item" icon="true">
                            <h6 class="btn" @click="deleteCartItem(item, index, records.cartItems)"> <icon icon="trash"/></h6>
                        </product>
                        <button type="button" class="btn btn-success btn-block" style="margin-top: 10px;" @click="checkout(records.cartItems)">Checkout</button>                   
                    </div>
                    <div v-else>
                        <h1 style="margin-bottom: 15px;">¿Your cart seems empty, want to add some items to it?</h1>
                        <button class="btn btn-info" @click="$router.go(-1)">Return Home</button>  
                    </div>
                </b-tab>
                <b-tab id="Orders" title="Orders">
                    <div v-if="!checkUserLogged.user.accountType">
                        <div v-if="checkOrderState">
                            <product v-for="order in records.clientOrders.products" :key="order.id" :product="order"></product>
                        </div>
                        <div v-else> <h1>There are no orders in progress</h1> </div>
                    </div>
                    <div v-else>
                        <order v-if="records.businessOrders.length" v-for="order in records.businessOrders" :key="order.id" :orders="order">
                            <div class="row">
                                <div class="col-md-6">
                                    <button class="btn btn-block btn-danger" @click="cancelOrder(order)">Cancel Order</button>
                                </div>
                                <div class="col-md-6">
                                    <button class="btn btn-block btn-success" @click="sendOrder(order)">Ship Order</button>
                                </div>
                            </div>
                        </order>
                        <div v-else> <h1>There are no pending orders</h1> </div>
                    </div>
                </b-tab>
                <b-tab title="Inventory" v-if="checkUserLogged.user.accountType">
                    <div class="row text-xs-center" v-if="records.inventory.length">
                        <div class="col-md-4" style="padding: 2%; width: 80%;" v-for="(product, index) in records.inventory" :key="product.id">
                            <input class="form-control" v-model.trim="product.name" :readonly="product.state !== 2">
                            <div class="card">
                                <img class="card-img-top" :src="product.image" alt="Card image cap" width="20%" height="15%">
                                <div class="card-body">
                                    <input class="form-control" v-model.trim="product.description" :readonly="product.state !== 2">
                                    <input class="form-control" v-model.trim="product.price" :readonly="product.state !== 2">
                                </div>
                                <button v-if="product.state === 2" class="btn btn-success btn-block" @click="editItem(product)"> <icon icon="edit"/> Edit </button>
                                <button v-if="product.state === 2" class="btn btn-warning btn-block" @click="cancelItemEditing(product)"> <icon icon="edit"/> Cancel </button>
                                <button v-if="product.state === 2" class="btn btn-danger btn-block" @click="deleteItem(product, index, records.inventory)"> <icon icon="trash"/> </button>
                                <button v-if="product.state === 1" class="btn btn-info btn-block" @click="enableItemEditing(product)"> <icon icon="edit"/> </button>
                            </div>
                        </div>
                    </div>
                    <div v-else>
                        <h3>There are no products in the inventory, want to add some?</h3>
                        <button class="btn btn-success" @click="records.page.tabIndex = 3">Add Product</button>   
                    </div>
                </b-tab>
                <b-tab title="Add Product" v-if="checkUserLogged.user.accountType">
                    <div class="form-control">
                        <div v-if="records.newProduct.image">
                            <img :src="records.newProduct.image" class="card-img-top"/>
                        </div> 
                        <div class="form-group">
                            <label>Name</label>
                            <input type="text" class="form-control" v-model.trim="records.newProduct.name">
                        </div>

                        <div class="form-group">
                            <label type="password">Description</label>
                            <input class="form-control" type="text" v-model.trim="records.newProduct.description">
                        </div>

                        <div class="form-group">
                            <label type="password">Image</label>
                            <input id="image" type="file" class="form-control" accept="image/*" @change="addNewProductImage($event)">
                        </div>

                        <div class="form-group">
                            <label>Price</label>
                            <input type="text" class="form-control" v-model.trim="records.newProduct.price">
                        </div>

                        <button type="submit" class="btn btn-success btn-block" style="margin-top: 10px;" @click="addItemToInventory(records.newProduct)">
                            <icon icon="plus"/> <span>Add</span>
                        </button>
                    </div>
                </b-tab>
            </b-tabs>
        </b-card>

        <img v-if="!checkUserLogged.loggedIn" src="https://firebasestorage.googleapis.com/v0/b/morse-code-7eb28.appspot.com/o/descarga.jpg?alt=media&token=c08c5d0f-0967-4607-9752-47ab03265f79" alt="Puerco" width="100%" height="100%">

    </div>
</template>

<script>
    import {api} from '../store/api';   
    import order from './order-template/order';
    import firebase from 'firebase';
    import product from '../components/product-template/product';
    import  VeeValidate from 'vee-validate';
    import store from '../store/index';

    export default {
        data() {
            return {
                records: {
                    page: { title: 'Profile', editing: false, tabIndex: 0 },
                    user: {},
                    clientOrders: {},
                    businessOrders: {},
                    paymentInfo: {},
                    inventory: [],
                    cartItems: [],
                    newProduct: { name: '', description: '', image: '', price: '', userId: '', uploadedDate: '', state: 1 },
                    originalUser: {}
                }
            }
        },
        components: {
            order,
            product
        },
        computed:{
            fullName() {
                return `${this.records.user.name} ${this.records.user.lastName}`; 
            },

            checkUserLogged() {
                return this.$store.state;
            },

            checkCartState() {
                if (this.records.cartItems) {
                    return this.records.cartItems.state === 1? 1 : 0; 
                }
            },

            checkOrderState() {
                if (this.records.clientOrders) {
                    return this.records.clientOrders.state === 2? 1 : 0; 
                }
            }
        },
        methods: {
            getUser() {
                this.records.user = this.checkUserLogged.user;
                this.records.originalUser = JSON.stringify(this.records.user);
            },

            getClientOrders(userId) {
                this.$http.get(`${api.url}cart?userId=${userId}&state=2`)
                    .then(res => {
                        this.records.clientOrders = res.data[0];
                    })
                    .catch(err => alert(err));
            },

            getBusinessOrders(userId) {
                this.$http.get(`${api.url}order?company=${userId}`)
                    .then(res => {
                        this.records.businessOrders = res.data;
                    })
                    .catch(err => alert(err));
            },

            getCartItems(userId) {
                this.$http.get(`${api.url}cart?userId=${userId}&state=1`)
                    .then(res => {
                        this.records.cartItems = res.data[0];
                    })
                    .catch(err => alert(err))     
            },

            getInventory(userId) {
                this.$http.get(`${api.url}product?userId=${userId}&state=1`)
                    .then(res => {
                        this.records.inventory = res.data;
                    })
                    .catch(err => alert(err));
            },
            
            addUserProfileImage(event, imageHolder) {
                let image = '';
                let t = event.target.files[0];
                let fileName = t.name;
                let storageRef = firebase.storage().ref(`/productImages/${fileName}`);
                let uploadTask = storageRef.put(t);
                uploadTask.on('state_changed', snapshot => {},
                    err => alert(err),
                    response => this.records.user.profilePicture = uploadTask.snapshot.downloadURL)
            },

            addNewProductImage(event) {
                let t = event.target.files[0];
                let fileName = t.name;
                let storageRef = firebase.storage().ref(`/productImages/${fileName}`);
                let uploadTask = storageRef.put(t);
                uploadTask.on('state_changed', snapshot => {},
                    err => alert(err),
                    response => this.records.newProduct.image = uploadTask.snapshot.downloadURL)
            },

            addItemToInventory(product) {
                if (product.name && product.price && product.description && product.image) {
                    if(confirm(`¿Are you sure about adding ${product.name} to the inventory?`)) {
                        product.userId = this.checkUserLogged.user.id;
                        product.date = new Date().toLocaleString();
                        this.$http.post(`${api.url}product`, product) 
                        .then(res => {
                            alert(`Item: ${product.name} with a price of RD$ ${product.price} has been successfully added to your inventory`);
                            this.getInventory(product.userId);
                            this.records.page.tabIndex = 1;
                        })
                        .catch(err => alert(err));
                    } else {
                        this.records.tabIndex = 2;
                    }
                } else {
                    alert('Please make sure too fill all fields');
                }
            },

            cancelOrder(order) {
                if (confirm(`¿Are you sure about cancelling order: ${order.id}?`)) {
                    order.state = 0;
                    this.$http.put(`${api.url}order/${order.id}`, order)
                        .then(res => {
                            alert(`Order ${order.id} has been Cancelled`);
                            this.getBusinessOrders(this.checkUserLogged.user.id);
                        })
                        .catch(err => alert(err));                
                }
            },

            cancelItemEditing(product) {
                if (confirm('Are you sure about cancel the edition?')) {
                    product.state = 1
                }
            },

            enableItemEditing(product) {
                product.state = 2;
            },

            sendOrder(order) {
                if (confirm(`¿Are you sure about shipping order: ${order.id}?`)) {
                    order.state = 2;
                    this.$http.put(`${api.url}order/${order.id}`, order)
                        .then(res => {
                            alert(`Order ${order.id} has been sent`);
                            this.getBusinessOrders(this.checkUserLogged.user.id);
                        })
                        .catch(err => alert(err));
                }
            },

            deleteItem(product, index, cart) {
                if (confirm(`Are you sure about deleting item ${product.name}`)) {
                    product.state = 0;
                    this.$http.put(`${api.url}product/${product.id}`, product)
                        .then(res => {
                            cart.splice(index, 1);
                            alert(`Item ${product.name} successfully removed`);
                        })
                        .catch(err => alert(err));
                }
            },

            deleteCartItem(item, index, cart) {
                if (confirm(`Are you sure about deleting this item`)) {
                    cart.products.splice(index, 1)
                    this.$http.put(`${api.url}cart/${cart.id}`, cart)
                    .then(res => alert(`Item successfully removed`))
                    .catch(err => alert(err));
                }
            },

            editItem(product) {
                if (confirm(`Are you sure about editing item ${product.name}`)) {
                    this.$http.put(`${api.url}product/${product.id}`, product)
                        .then(res => {
                            product.state = 1;
                            alert(`Item ${product.name} successfully updated`);
                        })
                        .catch(err => alert(err));
                } 
            },
            checkout(cart) {
                cart.state = 2;
                let tempOrder = {};
                this.$http.put(`${api.url}cart/${cart.id}`, cart)
                    .then(res => {
                        this.getClientOrders(this.checkUserLogged.user.id);
                        this.records.page.tabIndex = 2;
                        
                        cart.products.forEach(product => {
                            tempOrder = cart;
                            tempOrder = { 
                                products: [], 
                                product: [], 
                                company: product.userId, 
                                state: 1, 
                                date: new Date().toLocaleString(), 
                                userId: this.checkUserLogged.user.id 
                            };
                            tempOrder.product.push(product);

                            this.$http.post(`${api.url}order`, tempOrder)
                                .then(res => {
                                    cart = [];
                                    alert(`Order has been placed and will be delivered withing 3 business days`); 
                                })
                                .catch(err => alert(err));
                        });
                    })
                    .catch(err => alert(err));
            },

            updateUser(userData) {
                this.$validator.validateAll().then((result) => {
                    if (result) {
                        this.$http.put(`${api.url}user/${userData.id}`, userData)
                            .then(response => {
                                this.records.originalUser = JSON.stringify(userData);
                                localStorage.setItem('userData', this.records.originalUser);
                                alert('User sucessfuly update');
                                this.records.page.editing = false;
                            })
                            .catch(err => alert(err));
                    } else {
                        alert('Make sure all fields are properly filled');
                    }
                });

            },

            cancelEditing(product) {
                this.records.user = JSON.parse(this.records.originalUser);
                this.records.page.editing = false;
            },

            init(userId) {
                this.getUser();
                this.getClientOrders(userId);
                this.getBusinessOrders(userId);
                this.getCartItems(userId);
                this.getInventory(userId);
            }
        },
        created() {
            this.init(this.checkUserLogged.user.id);
        }
    }
</script>

<style>
    b-card {
        padding-bottom: 1.25rem;
    }
</style>
