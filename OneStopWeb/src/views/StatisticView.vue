<template>
    <div class="statistic">
        <div class="up">
            <div class="chart-container" style="background-color: #a2dbb1; padding: 0 2rem;">
                <div>

                    <Pie :data="pieData" :options="pieOptions" />
                </div>
            </div>
            <div class="chart-container" style="background-color: #a2c5db;">
                <h1>Processed / Total Request: </h1>
            </div>
            <div class="chart-container" style="background-color: #f2e9f2;">
                <h1>Total Cong Dan: </h1>
            </div>

        </div>
        <div class="down">
            <div class="chart-container" style="background-color: #ebe7e6;">
                <Bar :data="chartData" :options="chartOptions" />
            </div>
            <div class="chart-container" style="background-color: #ebe7e6;">
                <Bar :data="chartData" :options="chartOptions" />
            </div>
        </div>
    </div>
</template>

<script>
import { $notify } from '@/utils/variables';
import { computed, onMounted, ref } from 'vue';
import service from '../plugins/service';

import { Bar, Pie } from 'vue-chartjs';
import { Chart as ChartJS, Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale, ArcElement } from 'chart.js';

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale, ArcElement);


export default {
    name: 'BarChart',
    components: { Bar, Pie },
    setup() {
        //for cate bar chart
        const cateLabel = ref([]);
        const cateValue = ref([]);
        const chartData = computed(() => {
            return {
                labels: cateLabel.value,
                datasets: [
                    {
                        label: 'Data One',
                        backgroundColor: '#f87979',
                        data: cateValue.value,
                        borderRadius: 10
                    },
                ]
            };
        });
        const chartOptions = ref({
            // indexAxis: 'y',
            responsive: true,
            plugins: {
                legend: {
                    display: false
                }
            },
            scales: {
                x: {
                    display: false
                }
            }
        });

        const getCateStatistic = () => {
            try {
                service.statistic.getCateStatistic(
                    {},
                    (response) => {
                        cateLabel.value = response.map((item) => item.categoryName);
                        cateValue.value = response.map((item) => item.requestCount);
                    },
                    (error) => {
                        $notify.error('Error', error);
                    }
                )
            } catch (error) {
                $notify.error('Error', error);
            }
        }

        //for statistic pie chart
        const pieLabel = ref([]);
        const pieValue = ref([]);
        const pieData = computed(() => {
            return {
                labels: pieLabel.value,
                datasets: [
                    {
                        label: 'Data One',
                        backgroundColor: ['#f87979', '#f8e979', '#79f8e9', '#7979f8', '#f879f8'],
                        data: pieValue.value,
                        borderRadius: 10
                    },
                ]
            };
        });

        const pieOptions = ref({
            responsive: true,
            plugins: {
                legend: {
                    position: 'right'
                }
            }
        });



        onMounted(() => {
            getCateStatistic();
            pieLabel.value = ['Red', 'Yellow', 'Blue', 'Green', 'Purple'];
            pieValue.value = [12, 19, 3, 5, 2];
        });

        return {
            chartData,
            chartOptions,
            pieData,
            pieOptions,
        };
    }
};
</script>

<style scoped lang="scss">
.statistic {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    padding: 20px;

    .chart-container {
        height: 100%;
        margin-bottom: 10px;
        border-radius: 1rem;
        padding: 1rem;
        display: flex;
        justify-content: left;
    }

    .up {
        width: 100%;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        height: 25vh;
        margin-bottom: 1rem;

        .chart-container {
            width: 30%;
        }
    }

    .down {
        width: 100%;
        height: 50dvh;

        display: flex;
        flex-direction: row;

        .chart-container {
            margin: 8px;
            width: 100%;
            justify-content: end;
        }
    }
}
</style>
