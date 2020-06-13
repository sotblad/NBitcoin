using NBitcoin;
using NBitcoin.DataEncoders;
using NBitcoin.Protocol;
using NBitcoin.RPC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace NBitcoin.Altcoins
{
	public class MonetaryUnit : NetworkSetBase
	{
		public static MonetaryUnit Instance { get; } = new MonetaryUnit();

		public override string CryptoCode => "MUE";

		private MonetaryUnit()
		{

		}
		//Format visual studio
		//{({.*?}), (.*?)}
		//Tuple.Create(new byte[]$1, $2)
		static Tuple<byte[], int>[] pnSeed6_main = {
	Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x05,0x65,0x6a,0x35}, 19687),
	Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x52,0xc4,0x0b,0xc9}, 19687),
	Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x6b,0xaa,0xad,0x9d}, 19687),
	Tuple.Create(new byte[]{0xfd,0x87,0xd8,0x7e,0xeb,0x43,0x8c,0x14,0xde,0x2c,0x7d,0x6b,0xc0,0xa3,0xdc,0xf7}, 19687),
	Tuple.Create(new byte[]{0xfd,0x87,0xd8,0x7e,0xeb,0x43,0x9b,0x65,0xda,0xa4,0x3d,0xf5,0x2f,0x21,0x89,0xd5}, 19687),
	Tuple.Create(new byte[]{0xfd,0x87,0xd8,0x7e,0xeb,0x43,0x0d,0xfd,0x43,0xe6,0xeb,0xbc,0xc3,0xe4,0xf4,0x02}, 19687)
};
		static Tuple<byte[], int>[] pnSeed6_test = {
	Tuple.Create(new byte[]{0xfd,0x87,0xd8,0x7e,0xeb,0x43,0x8c,0x95,0x7f,0x9b,0x49,0x18,0xd4,0xca,0x06,0x11}, 19685),
};

#pragma warning disable CS0618 // Type or member is obsolete
		public class MonetaryUnitConsensusFactory : ConsensusFactory
		{
			private MonetaryUnitConsensusFactory()
			{
			}
			
			public static MonetaryUnitConsensusFactory Instance { get; } = new MonetaryUnitConsensusFactory();
			
			public override BlockHeader CreateBlockHeader()
			{
				return new MonetaryUnitBlockHeader();
			}
			public override Block CreateBlock()
			{
				return new MonetaryUnitBlock(new MonetaryUnitBlockHeader());
			}
		}
		
		public class MonetaryUnitBlockHeader : BlockHeader
		{
			public override uint256 GetPoWHash()
			{
				//TODO: Implement here
				throw new NotSupportedException();
			}
		}

		public class MonetaryUnitBlock : Block
		{
			public MonetaryUnitBlock(MonetaryUnitBlockHeader header) : base(header)
			{

			}

			public override ConsensusFactory GetConsensusFactory()
			{
				return MonetaryUnitConsensusFactory.Instance;
			}
		}

#pragma warning restore CS0618 // Type or member is obsolete

		protected override void PostInit()
		{
			RegisterDefaultCookiePath("MonetaryUnit");
		}

		protected override NetworkBuilder CreateMainnet()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				MajorityEnforceBlockUpgrade = 750,
				MajorityRejectBlockOutdated = 950,
				MajorityWindow = 1000,
				PowLimit = new Target(new uint256("00000fffff000000000000000000000000000000000000000000000000000000")),
				PowTargetTimespan = TimeSpan.FromSeconds(10 * 40),
				PowTargetSpacing = TimeSpan.FromSeconds(1 * 40),
				PowAllowMinDifficultyBlocks = false,
				CoinbaseMaturity = 100,
				PowNoRetargeting = false,
				RuleChangeActivationThreshold = 250,
				MinerConfirmationWindow = 1000,
				ConsensusFactory = MonetaryUnitConsensusFactory.Instance
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 16 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 76 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 126 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x02, 0x2D, 0x25, 0x33 })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x02, 0x21, 0x31, 0x2B })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("mue"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("mue"))
			.SetMagic(0xeafdc491)
			.SetPort(19687)
			.SetRPCPort(19688)
			.SetName("monetaryunit-main")
			.AddAlias("monetaryunit-mainnet")
			.AddAlias("monetaryunit-mainnet")
			.AddAlias("monetaryunit-main")
			.AddDNSSeeds(new[]
			{
				new DNSSeedData("dns1.monetaryunit.org", "dns1.monetaryunit.org"),
				new DNSSeedData("dns2.monetaryunit.org", "dns2.monetaryunit.org"),
				new DNSSeedData("dns3.monetaryunit.org", "dns3.monetaryunit.org")
			})
			.AddSeeds(new NetworkAddress[0])
			.SetGenesis("010000000000000000000000000000000000000000000000000000000000000000000000c787795041016d5ee652e55e3a6aeff6c8019cf0c525887337e0b4206552691613f7fc58f0ff0f1ea12400000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff4004ffff001d010438506f77657264652062792042697473656e642d4575726f7065636f696e2d4469616d6f6e642d4d41432d42332032332f4170722f32303137ffffffff010000000000000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000");
			return builder;
		}

		protected override NetworkBuilder CreateTestnet()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 210000,
				MajorityEnforceBlockUpgrade = 501,
				MajorityRejectBlockOutdated = 750,
				MajorityWindow = 1000,
				PowLimit = new Target(new uint256("00000fffff000000000000000000000000000000000000000000000000000000")),
				PowTargetTimespan = TimeSpan.FromSeconds(1 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(1 * 10),
				PowAllowMinDifficultyBlocks = true,
				CoinbaseMaturity = 240,
				PowNoRetargeting = false,
				ConsensusFactory = MonetaryUnitConsensusFactory.Instance
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 139 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 19 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x3A, 0x80, 0x61, 0xA1 })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x3A, 0x80, 0x58, 0x37 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("tmue"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("tmue"))
			.SetMagic(0xbd657647)
			.SetPort(19685)
			.SetRPCPort(19686)
		   .SetName("monetaryunit-test")
		   .AddAlias("monetaryunit-testnet")
		   .AddAlias("monetaryunit-test")
		   .AddAlias("monetaryunit-testnet")
		   .AddDNSSeeds(new[]
		   {
				new DNSSeedData("testnetdns.monetaryunit.org", "testnetdns.monetaryunit.org")
		   })
		   .AddSeeds(new NetworkAddress[0])
		   .SetGenesis("010000000000000000000000000000000000000000000000000000000000000000000000ff00e3481f61b255420602f7af626924221a41224b0d645bd2f082f82c8bc50a5746ff58f0ff0f1e98611a000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff4004ffff001d010438506f77657264652062792042697473656e642d4575726f7065636f696e2d4469616d6f6e642d4d41432d42332032332f4170722f32303137ffffffff01807c814a00000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000");
			return builder;
		}

		protected override NetworkBuilder CreateRegtest()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 150,
				MajorityEnforceBlockUpgrade = 750,
				MajorityRejectBlockOutdated = 950,
				MajorityWindow = 144,
				PowLimit = new Target(new uint256("00000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				PowTargetTimespan = TimeSpan.FromSeconds(24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(1 * 60),
				PowAllowMinDifficultyBlocks = false,
				PowNoRetargeting = false,
				ConsensusFactory = MonetaryUnitConsensusFactory.Instance
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 111 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 196 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x35, 0x87, 0xCF })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x35, 0x83, 0x94 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("rmue"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("rmue "))
			.SetMagic(0xad7ecfa2)
			.SetPort(19685)
			.SetRPCPort(19686)
			.SetName("monetaryunit-reg")
			.AddAlias("monetaryunit-regtest")
			.AddAlias("monetaryunit-regtest")
			.AddAlias("monetaryunit-reg")
			.AddDNSSeeds(new DNSSeedData[0])
			.AddSeeds(new NetworkAddress[0])
			.SetGenesis("010000000000000000000000000000000000000000000000000000000000000000000000c787795041016d5ee652e55e3a6aeff6c8019cf0c525887337e0b4206552691613f7fc58f0ff0f1ea12400000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff4004ffff001d010438506f77657264652062792042697473656e642d4575726f7065636f696e2d4469616d6f6e642d4d41432d42332032332f4170722f32303137ffffffff010000000000000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000");
			return builder;
		}
	}
}
