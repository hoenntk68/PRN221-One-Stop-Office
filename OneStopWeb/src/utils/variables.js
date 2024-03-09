import services from '@/plugins/service';
import mixins from '../helpers/mixins';
import { ElNotification } from 'element-plus'

export const mixinMethods = mixins.methods;
export const $notify = ElNotification;
export const $services = services;

export default {
    $services: services,
};
