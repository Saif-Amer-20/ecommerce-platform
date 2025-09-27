import React from 'react';
import { Card } from '../ui';

export const FeaturesSection: React.FC = () => {
  const features = [
    {
      icon: <ShippingIcon className="h-10 w-10" />,
      title: 'توصيل سريع ومجاني',
      description: 'توصيل مجاني للطلبات فوق 50,000 دينار عراقي إلى جميع أنحاء العراق',
      color: 'text-blue-600'
    },
    {
      icon: <PaymentIcon className="h-10 w-10" />,
      title: 'طرق دفع متنوعة',
      description: 'ادفع عند الاستلام أو عبر ZainCash أو AsiaHawala أو الحوالة البنكية',
      color: 'text-green-600'
    },
    {
      icon: <SupportIcon className="h-10 w-10" />,
      title: 'خدمة عملاء ممتازة',
      description: 'فريق خدمة العملاء متاح على مدار 24/7 لمساعدتك في أي وقت',
      color: 'text-purple-600'
    },
    {
      icon: <QualityIcon className="h-10 w-10" />,
      title: 'ضمان الجودة',
      description: 'جميع منتجاتنا أصلية مع ضمان الجودة وإمكانية الإرجاع خلال 14 يوم',
      color: 'text-orange-600'
    }
  ];

  return (
    <section className="py-16 bg-white">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="text-center mb-12">
          <h2 className="text-3xl font-bold text-gray-900 mb-4">لماذا تختار المتجر العربي؟</h2>
          <p className="text-lg text-gray-600 max-w-2xl mx-auto">
            نقدم لك تجربة تسوق فريدة مع أفضل الخدمات والضمانات
          </p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
          {features.map((feature, index) => (
            <FeatureCard key={index} feature={feature} />
          ))}
        </div>

        {/* Statistics */}
        <div className="mt-16 border-t border-gray-200 pt-12">
          <div className="grid grid-cols-1 md:grid-cols-4 gap-8 text-center">
            <div>
              <div className="text-3xl font-bold text-gray-900 mb-2">10,000+</div>
              <div className="text-gray-600">عميل راضٍ</div>
            </div>
            <div>
              <div className="text-3xl font-bold text-gray-900 mb-2">5,000+</div>
              <div className="text-gray-600">منتج متنوع</div>
            </div>
            <div>
              <div className="text-3xl font-bold text-gray-900 mb-2">24/7</div>
              <div className="text-gray-600">خدمة العملاء</div>
            </div>
            <div>
              <div className="text-3xl font-bold text-gray-900 mb-2">99%</div>
              <div className="text-gray-600">نسبة الرضا</div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

interface FeatureCardProps {
  feature: {
    icon: React.ReactNode;
    title: string;
    description: string;
    color: string;
  };
}

const FeatureCard: React.FC<FeatureCardProps> = ({ feature }) => {
  return (
    <Card className="text-center group hover:shadow-lg transition-shadow duration-300">
      <div className={`${feature.color} mb-4 flex justify-center group-hover:scale-110 transition-transform duration-300`}>
        {feature.icon}
      </div>
      <h3 className="text-xl font-bold text-gray-900 mb-3">
        {feature.title}
      </h3>
      <p className="text-gray-600 text-sm leading-relaxed">
        {feature.description}
      </p>
    </Card>
  );
};

// Feature Icons
const ShippingIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M8.25 18.75a1.5 1.5 0 01-3 0 1.5 1.5 0 013 0zM19.5 18.75a1.5 1.5 0 01-3 0 1.5 1.5 0 013 0zM3 7.5a.75.75 0 00-.75.75v1.5c0 .414.336.75.75.75h6.75V7.5H3zM4.5 12.75a.75.75 0 01.75-.75h13.5a.75.75 0 01.75.75v2.25a.75.75 0 01-.75.75H5.25a.75.75 0 01-.75-.75v-2.25zm7.5-9.75v6m-3-3l3-3 3 3" />
  </svg>
);

const PaymentIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M2.25 8.25h19.5M2.25 9h19.5m-16.5 5.25h6m-6 2.25h3m-3.75 3h15a2.25 2.25 0 002.25-2.25V6.75A2.25 2.25 0 0019.5 4.5h-15a2.25 2.25 0 00-2.25 2.25v10.5A2.25 2.25 0 004.5 19.5z" />
  </svg>
);

const SupportIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M7.5 8.25h9m-9 3H12m-9.75 1.51c0 1.6 1.123 2.994 2.707 3.227 1.129.166 2.27.293 3.423.379.35.026.67.21.865.501L12 21l2.755-4.133a1.14 1.14 0 01.865-.501 48.172 48.172 0 003.423-.379c1.584-.233 2.707-1.626 2.707-3.228V6.741c0-1.602-1.123-2.995-2.707-3.228A48.394 48.394 0 0012 3c-2.392 0-4.744.175-7.043.513C3.373 3.746 2.25 5.14 2.25 6.741v6.018z" />
  </svg>
);

const QualityIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M9 12.75L11.25 15 15 9.75m-3-7.036A11.959 11.959 0 013.598 6 11.99 11.99 0 003 9.749c0 5.592 3.824 10.29 9 11.623 5.176-1.332 9-6.03 9-11.622 0-1.31-.21-2.571-.598-3.751h-.152c-3.196 0-6.1-1.248-8.25-3.285z" />
  </svg>
);