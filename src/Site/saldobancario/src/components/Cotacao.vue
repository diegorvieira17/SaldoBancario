<template>
  <div class="cotacao">
    <div class="box-result">
      <label>Cotações do dia ({{dataCotacao}})</label>
      <button @click="consultaCotacoes()">Atualizar Cotações</button>
    </div>
    <md-card>
      <md-card-header>
        <div class="md-title">Dolar</div>
      </md-card-header>

      <md-card-content class="md-card-conten">
        <p>Venda: {{cotacoes.USD.sell}}</p>
        <p>Compra: {{cotacoes.USD.buy}}</p>
        <p>Variação: {{cotacoes.USD.variation}}%</p>
      </md-card-content>

      <md-card-actions></md-card-actions>
    </md-card>
    <md-card>
      <md-card-header>
        <div class="md-title">Euro</div>
      </md-card-header>

      <md-card-content class="md-card-conten">
        <p>Venda: {{cotacoes.EUR.sell}}</p>
        <p>Compra: {{cotacoes.EUR.buy}}</p>
        <p>Variação: {{cotacoes.EUR.variation}}%</p>
      </md-card-content>

      <md-card-actions></md-card-actions>
    </md-card>
    <md-card>
      <md-card-header>
        <div class="md-title">Bitcoin</div>
      </md-card-header>

      <md-card-content class="md-card-conten">
        <p>Venda: {{cotacoes.BTC.sell}}</p>
        <p>Compra: {{cotacoes.BTC.buy}}</p>
        <p>Variação: {{cotacoes.BTC.variation}}%</p>
      </md-card-content>

      <md-card-actions></md-card-actions>
    </md-card>
  </div>
</template>

<script>
import Vue from "vue";
import axios from '@/http'
import moment from "moment";
import "vue-material/dist/vue-material.min.css";
import "vue-material/dist/theme/default.css";
import { MdCard } from "vue-material/dist/components";

Vue.use(MdCard);

export default {
  name: "Cotações",
  data() {
    return {
      cotacoes: {
          USD: {
            buy: 0,
            sell: 0,
            variation: 0
          },
          EUR: {
            buy: 0,
            sell: 0,
            variation: 0
          },
          BTC: {
            buy: 0,
            sell: 0,
            variation: 0
          }
        },
        dataCotacao: ''
    };
  },
  created() {
    this.consultaCotacoes();
  },
  methods: {
    consultaCotacoes() {
      axios.get("cotacoes")
      .then(response => {
        console.log(response.data[0].dataCotacao);
        this.cotacoes.USD.buy = response.data[0].compra.toLocaleString("pt-br", {style: "currency", currency: "BRL"});
        this.cotacoes.USD.sell = response.data[0].venda.toLocaleString("pt-br", {style: "currency", currency: "BRL"});
        this.cotacoes.USD.variation = response.data[0].variacao.toLocaleString("pt-br", {currency: "BRL"});
        this.cotacoes.EUR.buy = response.data[1].compra.toLocaleString("pt-br", {style: "currency", currency: "BRL"});
        this.cotacoes.EUR.sell = response.data[1].venda.toLocaleString("pt-br", {style: "currency", currency: "BRL"});
        this.cotacoes.EUR.variation = response.data[1].variacao.toLocaleString("pt-br", {currency: "BRL"});
        this.cotacoes.BTC.buy = response.data[2].compra.toLocaleString("pt-br", {style: "currency", currency: "BRL"});
        this.cotacoes.BTC.sell = response.data[2].venda.toLocaleString("pt-br", {style: "currency", currency: "BRL"});
        this.cotacoes.BTC.variation = response.data[2].variacao.toLocaleString("pt-br", {currency: "BRL"});
        this.dataCotacao = moment(response.data[0].dataCotacao,"YYYYMMDDTHH:mm:ss.fffffffK").format("DD/MM/YYYY");
      })
    },
  }
};
</script>

<style scoped>
.md-card {
  width: 320px;
  margin: 4px;
  display: inline-block;
  vertical-align: top;
}
.md-title {
  text-align: left;
}
p
{
  margin: 0;
  padding: 0;
  font-size: 1.3em;
}
button {
  margin-bottom: 20px;
}
label {
  padding: 5px 0px 0px 0px
}
</style>