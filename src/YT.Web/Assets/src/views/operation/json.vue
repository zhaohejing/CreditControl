// expand-row.vue
<style scoped>
    .expand-row{
        margin-bottom: 16px;
    }
</style>
<template>
    <div>
        <Row class="expand-row">
           <tree-view :data="jsonObject" >
                </tree-view>
        </Row>
      
    </div>
</template>
<script>
    export default {
        props: {
            row: {
                type:String,
                required:true,
                default:""
            },
        },
        data(){
            return{
                jsonObject:null
             }
        },created(){
            this.jsonObject=JSON.parse(this.row);
        }
    };
</script>
