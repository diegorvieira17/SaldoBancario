<template>
  <div class="movimentacao">
    <h4>Movimentação Bancária</h4>
    <div class="table-limit">
      <table>
        <thead>
          <th>Data</th>
          <th>Descrição</th>
          <th>Valor</th>
        </thead>
        <tbody v-if="movimentos.length">
          <tr v-for="(movimento, index) in movimentos" :key="index">
            <td>{{movimento.dataMovimento}}</td>
            <td>{{movimento.descricao}}</td>
            <td>R$ {{movimento.valor}}</td>
          </tr>
        </tbody>
      </table>
    </div>

    <div class="box-result">
      <button @click="btnimportaClick()">Importar Arquivo Ofx</button>
      <label>Saldo: RS {{saldo}}</label>
    </div>

    <div class="box-result" v-if="mostaInput">
      <input ref="arquivo" class="upload" type="file" id="file" name="file" />
    </div>
    <div class="box-result" v-if="mostaInput">
      <button class="upload" @click="submitFile()">Enviar</button>
    </div>
  </div>
</template>

<script>
import Vue from "vue";
import axios from "@/http";
import moment from "moment";
import "vue-material/dist/vue-material.min.css";
import "vue-material/dist/theme/default.css";
import {
  MdTable,
  MdContent,
  MdRipple,
  MdButton
} from "vue-material/dist/components";

Vue.use(MdTable, MdContent, MdRipple);
Vue.use(MdContent);
Vue.use(MdRipple);
Vue.use(MdButton);

export default {
  name: "Extrato",
  data() {
    return {
      movimentos: {
        dataMovimento: "2011-06-27T12:00:00",
        descricao: "",
        valor: 0,
        saldoAtual: 0
      },
      saldo: 0,
      mostaInput: false,
      file: ""
    };
  },
  created() {
    this.consultaMovimentacoes();
  },
  methods: {
    btnimportaClick() {
      this.mostaInput = !this.mostaInput;
    },
    consultaMovimentacoes() {
      axios.get("movimentacoes").then(response => {
        this.movimentos = response.data;
        this.convertPtBr();
        if (response.data.length > 0) {
          this.saldo = this.movimentos[0].saldoAtual.toLocaleString("pt-br", {
            style: "currency",
            currency: "BRL"
          });
        }
      });
    },
    convertPtBr() {
      this.movimentos.forEach(movimento => {
        let valorPtBr = movimento.valor.toLocaleString("pt-br", {
          style: "currency",
          currency: "BRL"
        });
        movimento.valor = valorPtBr;
        movimento.dataMovimento = moment(
          movimento.dataMovimento,
          "YYYY-MM-DDThh:mm:ss"
        ).format("DD/MM/YYYY");
      });
      this.$set(this, "movimentos", this.movimentos);
    },
    submitFile() {
      let formData = new FormData();
      formData.append("file", this.$refs.arquivo.files[0]);
      axios
        .post("movimentacoes", formData, {
          headers: {
            "Content-Type": "multipart/form-data"
          }
        })
        .catch(ex => {})
        .then(response => {
          this.consultaMovimentacoes();
        });
    }
  }
};
</script>

<style scoped>
.movimentacao {
  padding: 0% 15px;
  margin: 0;
}
h4 {
  text-align: left;
  padding: 0 20px;
}
button {
  margin-top: 20px;
  margin-left: -15px;
}
label {
  padding-top: 20px;
}
input {
  padding-top: 20px;
}
</style>