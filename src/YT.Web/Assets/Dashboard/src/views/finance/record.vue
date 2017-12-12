<template>
    <div class="animated fadeIn">
        <Row>
            <milk-table ref="list" :layout="[22,2]" :columns="cols" :search-api="searchApi" :params="params">
                 <template slot="search">
                    <Form ref="params" :model="params" inline :label-width="60">
                        <FormItem label="公司名">
                            <Input v-model="params.name" placeholder="公司名"></Input>
                        </FormItem>
                        <FormItem label="金额">
                            <Input v-model="params.money" placeholder="金额"></Input>
                        </FormItem>
                        <FormItem label="开始时间">
                            <DatePicker type="date" :editable="false" v-model="params.start" placeholder="开始时间" style="width: 140px"></DatePicker>
                        </FormItem>
                        <FormItem label="截至时间">
                            <DatePicker type="date" :editable="false" v-model="params.end" placeholder="截至时间" style="width: 140px"></DatePicker>
                        </FormItem>
                    </Form>
                </template>
              
            </milk-table>
        </Row>
    </div>
</template>

<script>
import { records } from 'api/record';
export default {
    name: 'account',
    data() {
        return {
            cols: [
                {
                    title: '公司名',
                    key: 'customerName'
                },

                {
                    title: '充值金额',
                    key: 'chargeMoney'
                },
                {
                    title: '操作人',
                    key: 'actionName'
                },
                {
                    title: '创建时间',
                    key: 'creationTime',
                    render: (h, params) => {
                        return this.$fmtTime(params.row.creationTime);
                    }
                }
            ],
            searchApi: records,
            params: { name: '', money: '', start: null,end:null }
        }
    },
    created() {
       
    },
    destroyed() {
    },
    methods: {
        
    }
    
}
</script>


<style type="text/css" scoped>

</style>