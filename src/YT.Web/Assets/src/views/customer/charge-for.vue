<template>
    <milk-table ref="list" :columns="cols" :search-api="searchApi" :params="params">
        <template slot="search">
            <Form ref="params" :model="params" inline :label-width="70">
                <FormItem label="金额">
                    <InputNumber v-model="params.rmb" style="width:200px;" placeholder="请输入金额"></InputNumber>
                </FormItem>
            </Form>
        </template>
    </milk-table>
</template>
<script>
import { charge } from 'api/client';
import { getCards } from 'api/card';

export default {
    name: 'chargeFor',
    props: {
        clientId: {
            type: Number,
            default() {
                return null
            }
        }
    },
    data() {
        return {
            client: {
                id: this.clientId,
                money: null,
                cardCode: ''
            },
            cols: [
                {
                    type: 'index',
                    align: 'center',
                    width: '70px'
                },
                {
                    title: '充值卡卡号',
                    key: 'cardCode'
                },
                {
                    title: '金额',
                    key: 'money',
                    render: (h, params) => {
                        return params.row.money / 100;
                    }
                }
            ],
            searchApi: getCards,
            params: { rmb: null, state: false },
        }
    },
    created() {
    },
    mounted() {
    },
    computed: {
    },
    methods: {
        commit() {
            let card = this.$refs.list.current;
            if (card && card.money && card.cardCode) {
                this.client.money = card.money;
                this.client.cardCode = card.cardCode;
            } else {
                this.$Message.error('请选择充值卡!');
                return;
            }
            if (!this.client.id) {
                this.$Message.error('请选择要充值的账户!');
                this.$root.eventHub.$emit('charge');

            } else {
                charge(this.client).then(r => {
                    if (r.data.success) {
                        this.$Message.success('充值成功!');
                        this.$root.eventHub.$emit('charge');
                    } else {
                        this.$Message.success('充值失败!');
                        this.$root.eventHub.$emit('charge');
                    }
                });
            }

        }
    }
}
</script>
