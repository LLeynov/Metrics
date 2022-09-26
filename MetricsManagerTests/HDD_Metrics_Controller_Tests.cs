﻿using MetriscManager.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTests
{
    public class HDD_Metrics_Controller_Tests
    {
        private HDD_Metrics_Controller _hddMetricsControllerTests;
        public HDD_Metrics_Controller_Tests()
        {
            _hddMetricsControllerTests = new HDD_Metrics_Controller();
        }

        [Fact]
        public void GetHDDMetricsFromAgent()
        {
            //Готовим данные для теста
            int agentId = 1;
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение самого метода
            var result = _hddMetricsControllerTests.GetHDDMetricsFromAgent(agentId, timeFrom, timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetHDDMetricsFromAll_ReturnOk()
        {
            //Готовим данные для метода
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение метода
            var result = _hddMetricsControllerTests.GetHDDMetricsFromAll(timeFrom, timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
