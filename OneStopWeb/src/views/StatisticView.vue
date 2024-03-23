<template>
    <div class="statistic">
        <div class="up">
            <div class="chart-container" style="background-color: #a2dbb1; padding: 0 2rem;">
                <div>

                    <Pie :data="pieData" :options="pieOptions" />
                </div>
            </div>
            <div class="chart-container" style="background-color: #a2c5db;">
                <!-- {{ timeStat }} -->
                <table v-if="timeStat">
                    <tr>
                        <th>Thời gian xử lí trung bình: </th>
                        <td>{{ timeStat.avg.hourCount }} : {{ timeStat.avg.minuteCount }}</td>
                    </tr>
                    <tr>
                        <th>Thời gian xử lí nhanh nhất: </th>
                        <td>{{ timeStat.min.hourCount }} : {{ timeStat.avg.minuteCount }}</td>
                    </tr>
                    <tr>
                        <th>Thời gian xử lí chậm nhất: </th>
                        <td>{{ timeStat.max.hourCount }} : {{ timeStat.avg.minuteCount }}</td>
                    </tr>
                </table>
            </div>
            <div class="chart-container" style="background-color: #f2e9f2; text-align: left;">
                <table>
                    <tr>
                        <th>Số request: </th>
                        <td>{{ generalStat.requestCount }}</td>
                    </tr>
                    <tr>
                        <th>Số dân: </th>
                        <td>{{ generalStat.adminCount }}</td>
                    </tr>
                    <tr>
                        <th>Số nhân viên: </th>
                        <td>{{ generalStat.clientCount }}</td>
                    </tr>
                    <tr>
                        <th>Số người dùng: </th>
                        <td>{{ generalStat.userSystemCount }}</td>
                    </tr>
                    <tr>
                        <th>Số loại hồ sơ phục vụ: </th>
                        <td>{{ generalStat.categoryCount }}</td>
                    </tr>
                </table>
            </div>

        </div>
        <div class="down">
            <div class="chart-container" style="background-color: #ebe7e6;">
                <Bar :data="chartData" :options="chartOptions" />
            </div>
            <div class="chart-container" style="background-color: #ebe7e6;">
                <Bar :data="staffData" :options="staffOptions" />
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
                        label: 'số yều cầu / loại hồ sơ',
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
                    position: 'bottom'
                },
                title: {
                    display: true,
                    text: 'Các loại hồ sơ phục vụ',
                    font: {
                        size: 16
                    },
                    position: 'bottom',
                },
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
                        label: '',
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

        //for staff
        const staffLabel = ref([]);
        const staffValue = ref([]);
        const staffData = computed(() => {
            return {
                labels: staffLabel.value,
                datasets: [
                    {
                        label: 'số yêu cầu được giao cho',
                        backgroundColor: '#f87979',
                        data: staffValue.value,
                        borderRadius: 10
                    },
                ]
            };
        });

        const staffOptions = ref({
            indexAxis: 'y',
            responsive: true,
            plugins: {
                legend: {
                    position: 'bottom'
                },
                title: {
                    display: true,
                    text: 'Top nhân viên xuất sắc',
                    position: 'bottom',
                }
            },
            scales: {
                x: {
                    // display: false
                }
            }
        });

        const getStaffEf = () => {
            try {
                service.statistic.getStaffEf(
                    {},
                    (response) => {
                        staffLabel.value = response.map((item) => item.fullName);
                        staffValue.value = response.map((item) => item.requestCount);
                    },
                    (error) => {
                        $notify.error('Error', error);
                    }
                )
            } catch (error) {
                $notify.error('Error', error);
            }
        }

        const generalStat = ref('');

        const getGeneralStat = () => {
            try {
                service.statistic.getGeneralStat(
                    {},
                    (response) => {
                        console.log('response', response);
                        generalStat.value = response;
                    },
                    (error) => {
                        $notify.error('Error', error);
                    }
                )
            } catch (error) {
                $notify.error('Error', error);
            }
        }

        const timeStat = ref('');
        const getTimeStat = () => {
            try {
                service.statistic.getTimeStat(
                    {},
                    (response) => {
                        timeStat.value = response;
                    },
                    (error) => {
                        $notify.error('Error', error);
                    }
                )
            } catch (error) {
                $notify.error('Error', error);
            }
        }

        const getPieData = () => {
            try {
                service.statistic.getStatusState(
                    {},
                    (response) => {
                        pieLabel.value = response.map((item) => item.status);
                        pieValue.value = response.map((item) => item.requestCount);
                    },
                    (error) => {
                        $notify.error('Error', error);
                    }
                )
            } catch (error) {
                $notify.error('Error', error);
            }
        }

        onMounted(() => {
            getCateStatistic();
            getStaffEf();
            getGeneralStat();
            getPieData();
            getTimeStat();
            // pieLabel.value = ['Red', 'Yellow', 'Blue', 'Green', 'Purple'];
            // pieValue.value = [12, 19, 3, 5, 2];
        });

        return {
            chartData,
            chartOptions,
            pieData,
            pieOptions,
            staffData,
            staffOptions,
            getGeneralStat,
            generalStat,
            getTimeStat,
            timeStat,
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

        td {
            font-size: 1.2rem;
            font-weight: bold;
            padding-left: 2rem;
        }
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
