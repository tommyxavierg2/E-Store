<template>
  <div>
      <form>
          <div class="form-group" v-for="field in formFields" :key="field.name">
            <label v-if="!field.class">{{field.name}} </label>
            <p :class="{ 'control': true }" v-if="!field.class">
                <input v-if="field.type !== 'select'" :name="field.name" v-model="field.data" :class="{ 'form-control': true, 'text-danger': errors.has(field.name) }" :type="field.type"  :placeholder="field.placeholder" v-validate="`${field.required}`" :readonly="field.readonly">
                <select v-else v-model="field.data" class="form-control">
                    <label :for="field.name"></label>
                    <option v-for="option in field.options" :key="option.name" :id="option.name" :value="option.data" >{{option.name}}</option>
                 </select>
                <span class="text-danger" v-show="errors.has(field.name)">{{ errors.first(field.name) }}</span>
            </p>
            <label v-if="field.class"> <input :type="field.type" :class="field.class" v-model="field.data">{{field.name}}</label>
          </div>
          <div class="row">
              <div :class="'col-' + (12 / buttons.length)" v-for="btn in buttons" :key="btn.name">
                  <button type="button" :class="btn.class" @click="validateBeforeSubmit(getFieldsData(formFields), btn.action)">{{btn.name}}</button>
              </div>
              <div class="space"></div>
          </div>
      </form>
  </div>
</template>

   <!-- /*For using the 'Easy-Peasy' template you're going to need: 
        [{fields}]: (PAY ATTENTION TO THIS ONE AS IT'S VERY IMPORTANT) prop for passing the data that's going to be iterated in the component, 
        you'll going to need an ARRAY filled with OBJECTS in which each one NEEDS to have the properties: 
       {'name'}: for the label and validations of the field, a {'type'} for the type of data you want to fill in each input field for example: a 'text' input field, an 'email' input field or a 'password' input field,
        in the case you would like to add a checkbox, remember to add the {'checkbox}' type and a new atribute to the object called class, within have a 'checkbox' string that will set up the checkbox properly,
         a {'required'} string for the required atribute on the input that's going to validate if a field is needed by the form (this should be passed in a string as follow, example: 'required', 
         then a {'readonly'} boolean ('true' or 'false') for the readonly atribute that set's the input field as editable/readonly in the form.

        [{buttons}]: prop for passing a button's behavior, The {'name'} property for the name that the button is going to show, example: 'Register', 'Login', 'Update', the {'class'} atribute for the 
        design you want them to have for example: 'btn btn-danger', if no class want to be applied just pass an empty string and last the {'action'} property which is the function that a button will be performing
        on click, in the case an empty string is passed it will take the default action of routing the user to the default homepage.
    */ -->

<script>
    import  VeeValidate from 'vee-validate';
    
    export default {
        data() {
            return {}
        },
        props: ['formFields', 'buttons'],
        methods: {
            getFieldsData(formData) {
                let fieldsData = {};
                formData.forEach(field => {
                    fieldsData[field.name] = field.data;
                });
                return fieldsData; 
            },

            validateBeforeSubmit(func, action) {
                this.$validator.validateAll().then((result) => {
                    if (result) {
                        action(func);                            
                    } else if (!func) {
                        action;
                    } else {
                        this.$router.replace('/');
                    }
                });
            }
        },
    }
</script>

<style>
    .space { margin:0; padding:0; height:70px; }
</style>



