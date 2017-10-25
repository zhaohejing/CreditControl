<template>
    <div class="animated fadeIn">
        <Row>
            <milk-table :layout="[20,2,2]" ref="list" :columns="cols" :search-api="searchApi" :params="params">
                <template slot="search">
                    <Form ref="params" :model="params" inline :label-width="60">
                        <FormItem label="用户姓名">
                            <Input v-model="params.userName" placeholder="用户姓名"></Input>
                        </FormItem>
                        <FormItem label="操作时间">
                            <DatePicker type="date" format="yyyy-MM-dd" :clearable="false" style="width:140px;" placeholder="起始时间" v-model="params.startDate"></DatePicker>-
                            <DatePicker type="date" format="yyyy-MM-dd" :clearable="false" style="width:140px;" placeholder="截止时间" v-model="params.endDate"></DatePicker>
                        </FormItem>
                        <FormItem label="操作模块">
                            <Input v-model="params.serviceName" placeholder="操作模块"></Input>
                        </FormItem>
                    </Form>
                </template>
                <template slot="actions">
                    <Button @click="toExcel" type="primary">导出</Button>
                </template>
            </milk-table>
        </Row>
    </div>
</template>

<script>
import { getAudits, getAuditsToExcel } from 'api/audit';
import jsonView from './json';
export default {
    name: 'audit',
    data() {
        return {
            cols: [
                {
                    type: 'expand',
                    align: 'center',
                    width: '70px',
                      render: (h, params) => {
                            return h(jsonView, {
                                props: {
                                    row: params.row.parameters
                                }
                            })
                        }
                },
                {
                    title: '用户姓名',
                    key: 'userName'
                },
                {
                    title: '功能模块',
                    key: 'serviceName'
                },
                {
                    title: '动作',
                    key: 'methodName'
                },
                {
                    title: '操作时间',
                    key: 'executionTime',
                    render: (h, params) => {
                        return this.$fmtTime(params.row.executionTime);
                    }
                }
            ],
            searchApi: getAudits,
            params: { startDate: new Date(), endDate: new Date(), userName: '', serviceName: '' }
        }
    },
    created() {

    },
    components: {
        jsonView
    },
    destroyed() {
    },
    methods: {
        toExcel() {
        }
    },
    mounted() {
    }
}
</script>


<style type="text/css" scoped>

</style>